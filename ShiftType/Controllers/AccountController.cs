using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShiftType.DbModels;
using ShiftType.Models;
using ShiftType.Services;

namespace ShiftType.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly TypingDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AccountController(UserManager<User> userManager,TypingDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("/account/account")]
        public async Task<IActionResult> Account()
        {
        
            var user = await _userManager.GetUserAsync(User);
   var profile = ProfileInfoService.GenerateInfo(user,_context);
            return View(profile);
        }
        [HttpPost("/profile/edit")]
        public async Task<IActionResult> Edit(ProfileEditModel model) 
        {
            var user = await _userManager.GetUserAsync(User);
            user.VisibleName = model.Name;
            user.Description = model.Description;
            if (model.Image != null)
            {
                ImageHelperService.Upload(model.Image, user, _context, _webHostEnvironment).Wait();
            }
            user.Badge = model.BadgeId == -1 ? null : _context.Badges.FirstOrDefault(x=> x.Id == model.BadgeId);
           await  _context.SaveChangesAsync();
            return Ok();
        }
    }
}
