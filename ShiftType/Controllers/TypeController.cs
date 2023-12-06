using Microsoft.AspNetCore.Mvc;
using ShiftType.DbModels;
using ShiftType.Models;
using System;
using System.Text.Json;
using ShiftType.Services;
using Microsoft.AspNetCore.Identity;

namespace ShiftType.Controllers
{
    public class TypeController : Controller
    {
        private readonly TypingDbContext _context;
        private readonly UserManager<User> _userManager;
        public TypeController(TypingDbContext typingDbContext,UserManager<User> userManager)
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
           var result = _context.Results.First(x=> x.Id == id);

            return View(result);
        }
        [HttpPost("type/getTest")]
        public IActionResult GetTest([FromBody] Modifiers modifiers)
        {
            var text = "";
            var random = new Random();
            string[] words;
            switch (modifiers.TestType)
            {
                case TestTypes.Time:
                    words = TestProviderService.GetWords(modifiers.Language);
                    for (int i = 0; i < 4 * modifiers.TimeAmount; i++)
                    {
                        text += words[random.Next(0, words.Length - 1)] + " ";
                    }
                    break;
                case TestTypes.Words:
                    words = TestProviderService.GetWords(modifiers.Language);
                    for (int i = 0; i < modifiers.WordCount; i++)
                    {
                        text += words[random.Next(0, words.Length - 1)] + " ";
                    }
                    break;
                case TestTypes.Quote:
                    text += TestProviderService.GetRandomQuote(modifiers.Language, (QuoteType)modifiers.QuoteType);
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

            result.Wpm = TypeHelperService.CountWPM(input,check, (double)resultModel.TimeSpent);
            result.TimeSpent = (double)resultModel.TimeSpent;

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

                UserLevelService.AddExp(user, (result.TypedText.Length - result.Errors) / 3);
            }
            _context.Results.Add(result);
            _context.SaveChanges();
            return Json(result.Id);
        }
    }
}
