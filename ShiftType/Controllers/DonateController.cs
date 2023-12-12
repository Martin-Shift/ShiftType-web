using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Style;
using ShiftType.DbModels;
using ShiftType.Models;
using System.Net.Mail;
using System.Net;
using System;

namespace ShiftType.Controllers
{
    public class DonateController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly TypingDbContext _context;
        private readonly ShiftType.Models.LiqPay _liqPay;
        public DonateController(UserManager<User> userManager, TypingDbContext context, Models.LiqPay liqPay)
        {
            _userManager = userManager;
            _context = context;
            _liqPay = liqPay;
        }

        [HttpPost("/donate/checkout")]
        public async Task<IActionResult> Checkout([FromForm] decimal amount)
        {
            return RedirectToAction("Payment", new { amount = amount });
        }
        [HttpGet]
        public async Task<IActionResult> Payment(decimal amount)
        {
            var param = _liqPay.PayParams(amount, "Support Creator", Guid.NewGuid().ToString());
            ViewData["amount"] = amount;
            ViewData["data"] = _liqPay.GetData(param);
            ViewData["signature"] = _liqPay.GetSignature(param);
            return View("Payment");
        }
        [HttpPost("/donate/submit")]
        public async Task<IActionResult> SubmitPayment([FromBody] Notify notify)
        {
            var user = await _userManager.GetUserAsync(User);

            var message = System.IO.File.ReadAllText("D:\\Mein progectos\\Shift Tech\\Shift Tech\\bin\\Debug\\net6.0\\thanksLetter.html");
            
            try
            {
                var smtpclient = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("mori.steamer@gmail.com", System.IO.File.ReadAllText("D:\\SecureFiles\\SmtpPassword.txt")),
                    EnableSsl = true,
                };
                var mail = new MailMessage()
                {
                    IsBodyHtml = true,
                    From = new MailAddress("mori.steamer@gmail.com", "ShiftType"),
                    Subject = "Shift Tech",
                   Body = message,
                };
                mail.To.Add($"{user.Email}");
                smtpclient.Send(mail);

                var donate = new Donate()
                {
                    User = user,
                    Amount = notify.Amount
                };
                _context.Donates.Add(donate);
            }
            catch (Exception)
            {

            }
            _context.SaveChanges();
            return Ok();
        }
    }
}
