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
        public AccountController(UserManager<User> userManager,TypingDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [HttpGet("/profile/profile")]
        public async Task<IActionResult> Profile()
        {
        
            var user = await _userManager.GetUserAsync(User);
   var profile = ProfileInfoService.GenerateInfo(user,_context);
            return View(profile);
        }
        [HttpPost("/profile/edit")]
        public IActionResult Edit() 
        {

            return Ok();
        }
    }
}
