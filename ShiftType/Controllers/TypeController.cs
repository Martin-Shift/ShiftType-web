using Microsoft.AspNetCore.Mvc;
using ShiftType.DbModels;
using ShiftType.Models;
using System;
using System.Text.Json;
using ShiftType.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ShiftType.Controllers
{
    public class TypeController : Controller
    {
        private readonly TypingDbContext _context;
        private readonly UserManager<User> _userManager;
        public TypeController(TypingDbContext typingDbContext, UserManager<User> userManager)
        {
            _context = typingDbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("type/results/{id}")]
        public IActionResult Results(int id)
        {
            var result = _context.Results.First(x => x.Id == id);

            return View(result);
        }
        [HttpPost("type/getTest")]
        public IActionResult GetTest([FromBody] Modifiers modifiers)
        {          
            var text = "";
            switch (modifiers.TestType)
            {
                case TestTypes.Time:
                    text = TestProviderService.GetWords(modifiers.Language,modifiers.TimeAmount*5,modifiers.IsSymbols,modifiers.IsNumbers);
                    break;
                case TestTypes.Words:
                    text = TestProviderService.GetWords(modifiers.Language,modifiers.WordCount, modifiers.IsSymbols, modifiers.IsNumbers);
                    break;
                case TestTypes.Quote:
                    text += TestProviderService.GetRandomQuote(modifiers.Language, (QuoteType)modifiers.QuoteType, _context).Text;
                    break;
                case TestTypes.Custom:
                    text = modifiers.CustomText;
                    break;
                default:
                    break;
            }
            return Ok(new { Test = text });
        }

        [HttpPost("type/submit")]
        public async Task<IActionResult> SubmitResult([FromBody] ResultModel resultModel)
        {
            var result = new Result();
            var input = string.Join("", resultModel.TypedText);
            var check = string.Join("", resultModel.OriginalText);
            result.TypedText = input;
            result.Text = check;
            result.TestType = (int)resultModel.Type;

            result.Wpm = TypeHelperService.CountWPM(input, check, (double)resultModel.TimeSpent);
            result.TimeSpent = resultModel.TimeSpent ?? 0;

            var wordsArr = resultModel.TypedSeconds.Select(x => string.Join("", x)).ToArray();
            List<int> wpm = new() { };
            for (int i = 0; i < wordsArr.Length; i++)
            {
                wpm.Add(TypeHelperService.CountWPM(wordsArr[i], check, i + 1));
            }
           result.TypedSeconds = JsonSerializer.Serialize(wpm);
            result.Errors = TypeHelperService.CountErrors(input, check);
            result.Date = DateTime.Now;

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                result.User = user;

                UserLevelService.AddExp(user, (int)((result.TypedText.Length - result.Errors) / 3 * ((resultModel.IsNumbers ?? false) ? 1.2 : 1) * ((resultModel.IsSymbols ?? false) ? 1.2 : 1)));
            }
            _context.Results.Add(result);
            _context.SaveChanges();
            return Json(result.Id);
        }

        [Authorize]
        [HttpGet("type/leaderboard")]
        public async Task<IActionResult> RenderLeaderboard(int seconds)
        {
            var users = _context.Users
                .Include(x => x.Results)
                .Where(x => x.Results
                .Any(x => x.TimeSpent == seconds))
                .Select(x => ProfileInfoService.GenerateInfo(x, _context))
                .ToList()
                .OrderByDescending(x => ResultProviderService.GetBestTimeResult(x.Results.ToList(), seconds).Wpm)
                .ToList();
  var user = ProfileInfoService.GenerateInfo(await _userManager.GetUserAsync(User),_context);   
            ViewData["Seconds"] = seconds;
            if (seconds == 15)
            {
                return PartialView("Partials/_LeftLeaderboardPartial", new { Users = users, CurrentUser = user });
            }
            else
            {
                return PartialView("Partials/_RightLeaderboardPartial", new { Users = users, CurrentUser = user });
            }
        }

        [Authorize]
        [HttpPost("quote/submit")] 
        public async Task<IActionResult> SubmitQuote(Quote quote)
        {
            var user = await _userManager.GetUserAsync(User);
            if (!_context.Quotes.Any(x=> x.Text == quote.Text)) {
                quote.Publisher = user;
                _context.Quotes.Add(quote);
                _context.SaveChanges();
                return Ok(new {Message = "Success!"});
            }
            return BadRequest(new { Message = "Quote Already Exists!" });


        }
    }
}
