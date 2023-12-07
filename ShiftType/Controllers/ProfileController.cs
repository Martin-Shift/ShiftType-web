using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftType.DbModels;
using ShiftType.Services;
using System.Security.Cryptography.X509Certificates;

namespace ShiftType.Controllers
{
    public class ProfileController : Controller
    {
        private readonly TypingDbContext _context;
        public ProfileController(TypingDbContext context)
        {
            _context = context;
        }
        [HttpGet("profile/profile/{id}")]
        public IActionResult Profile(int id)
        {
            var user = ProfileInfoService.GenerateInfo(_context.Users.First(x => x.Id == id),_context);
            return View(user);
        }
    }
}
