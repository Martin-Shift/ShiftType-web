using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShiftType.DbModels;
using ShiftType.Services;
using System.Security.Claims;

namespace ShiftType.Controllers
{
    public class GoogleAuthController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public GoogleAuthController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("GoogleLogin")]
        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            var redirectUrl = Url.Action("GoogleLoginCallback", "Account", null, protocol: HttpContext.Request.Scheme);
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return Challenge(properties, "Google");
        }

        [HttpGet("GoogleLoginCallback")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleLoginCallback()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login");
            }
            var user = await _userManager.FindByEmailAsync(info.Principal.FindFirstValue(ClaimTypes.Email));
            var login = UserInfoService.GetFirstPartOfEmail(info.Principal.FindFirstValue(ClaimTypes.Email));
            if (user == null)
            {
                user = new User
                {
                    UserName = login,
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                    VisibleName = info.Principal.FindFirstValue(ClaimTypes.Name)
                };
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
            }
            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Shop");
        }
    }
}
