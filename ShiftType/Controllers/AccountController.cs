using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using ShiftType.DbModels;
using ShiftType.Models;
using ShiftType.Services;
using System.Linq.Expressions;

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

        [HttpGet("/account/exportExcel")]
        public async Task<IActionResult> ExportExcel()
        {
            try{
                var package = new ExcelPackage();  
                // Create a worksheet
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                worksheet.Cells["A1"].Value = "Id";
                worksheet.Cells["B1"].Value = "Wpm";
                worksheet.Cells["C1"].Value = "Raw";
                worksheet.Cells["D1"].Value = "Accuracy";
                worksheet.Cells["E1"].Value = "Consistency";
                worksheet.Cells["F1"].Value = "Written Text";
                worksheet.Cells["G1"].Value = "Original Text";
                worksheet.Cells["H1"].Value = "Type";
                worksheet.Cells["I1"].Value = "Date";
                var user = await _userManager.GetUserAsync(User);
                int row = 2;
                foreach (var result in _context.Results.Where(x=> x.User.Id == user.Id) )
                {
                    worksheet.Cells[row, 1].Value = result.Id;
                    worksheet.Cells[row, 2].Value = result.Wpm;
                    worksheet.Cells[row, 3].Value = result.Wpm + result.Errors;
                    worksheet.Cells[row, 4].Value = result.Accuracy;
                    worksheet.Cells[row, 5].Value = result.Consistency;
                    worksheet.Cells[row, 6].Value = result.TypedText;
                    worksheet.Cells[row, 7].Value = result.Text;
                    worksheet.Cells[row, 8].Value = (TestTypes)result.TestType;
                    worksheet.Cells[row, 9].Value = result.Date.ToString("dd.MM.yyyy hh:mm:ss");
                    row++;
                }

                var fileName = $"results_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                byte[] fileBytes = package.GetAsByteArray();

                // Return the Excel file directly as a FileResult
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

                // Write the Excel file to the response
            }
            catch (Exception ex)
            {
                return Ok();
            }
            
        }
    }
}
