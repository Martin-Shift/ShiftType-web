using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShiftType.DbModels;
using ShiftType.Models;

namespace ShiftType.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("Auth/Login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet("Auth/Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("Auth/SubmitLogin")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var existingEmail = await _userManager.FindByEmailAsync(request.LoginOrEmail);
                var existingLogin = await _userManager.FindByNameAsync(request.LoginOrEmail);
                if (existingLogin == null && existingEmail == null)
                {
                    return BadRequest(new { Message = "", Error = "No Such Login Exists" });
                }
                if (existingEmail != null) { request.LoginOrEmail = existingEmail.UserName; }

                var result = await _signInManager.PasswordSignInAsync(request.LoginOrEmail, request.Password, request.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.GetUserAsync(User);
                    var editPageUrl = "/";


                    if (!string.IsNullOrEmpty(editPageUrl))
                    {
                        return Ok(new { Message = "Success" });
                    }
                    else
                    {
                        return Ok(new { Message="Success" });
                    }
                }
                else
                {
                    return BadRequest(new { Message = "", Error = "Wrong Password!" });
                }
            }

            return View(request);
        }
        [HttpPost("Auth/SubmitRegister")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            foreach (var key in ModelState.Keys)
            {
                var entry = ModelState[key];
                if (entry.Errors.Any())
                {
                    foreach (var error in entry.Errors)
                    {
                        // You can log or print the validation error messages here
                        Console.WriteLine($"Property: {key}, Error: {error.ErrorMessage} ");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                var existingLogin = await _userManager.FindByNameAsync(request.Login);
                if (existingLogin != null)
                {
                    ModelState.AddModelError(string.Empty, "The login is already in use.");
                }

                var existingEmail = await _userManager.FindByEmailAsync(request.Email);
                if (existingEmail != null)
                {
                    ModelState.AddModelError(string.Empty, "The email is already in use.");
                }

                if (ModelState.ErrorCount > 0)
                {
                    // Return the error messages as JSON response
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return BadRequest(new { Errors = errors });
                }

                var user = new User { VisibleName = request.Login, UserName = request.Login, Email = request.Email };
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    var login = await _signInManager.PasswordSignInAsync(request.Email, request.Password, true, lockoutOnFailure: false);
                    return RedirectToAction("Index","Type");
                }

                // Handle other registration errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() });
        
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
