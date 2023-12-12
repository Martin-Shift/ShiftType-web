using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftType.DbModels;
using ShiftType.Models;
using ShiftType.Services;

namespace ShiftType.Controllers
{
    public class AboutController : Controller
    {
        private readonly TypingDbContext _context;
        public AboutController(TypingDbContext context)
        {
            _context = context;
        }
        [HttpGet("/about")]
        public IActionResult Index()
        {
            var users = _context.Users
                .Include(x => x.Results)
                .ToList()
                .Select(x => ResultProviderService
                .AverageBy(x.Results.ToList(), x => x.Wpm))
                .GroupBy(x => ((int)x / 10) * 10)
                .OrderBy(x => x.Key);
            var info = new AboutInfo();
           info.UserWpmChart = users
                .Select(x => x.Count())
                .ToList();
            info.UserWpmChartLabels =
               users
                .Select(g => g.Key)
                .Select(i => $"{i}-{i + 9}")
                .ToList();
            info.TotalUsers = _context.Users.Count();
            info.TotalTypingTime = (int)_context.Results.Select(x => x.TimeSpent).ToList().Sum();
            info.TotalTests = _context.Results.Count();
            return View(info);
        }
    }
}
