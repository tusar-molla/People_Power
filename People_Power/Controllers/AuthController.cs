using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using People_Power.Interfaces;
using People_Power.Models;
using People_Power.ViewModel;
using System.Security.Claims;

namespace People_Power.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AuthController(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the email is already registered
                if (await _userRepository.IsEmailExistsAsync(model.Email))
                {
                    ModelState.AddModelError("", "Email is already in use.");
                    return View(model);
                }

                if (await _userRepository.IsUserNameExistsAsync(model.UserName))
                {
                    ModelState.AddModelError("", "Username is already in use.");
                    return View(model);
                }

                // Create a new user
                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PasswordHash = _passwordHasher.HashPassword(model.Password),
                    RoleId = 2
                };

                // Save user to the database
                var result = await _userRepository.CreateUserAsync(user);

                if (result)
                {
                    TempData["SuccessMessage"] = "Registration successful! Please log in.";
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError("", "An error occurred while registering. Please try again.");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            var Passhash = _passwordHasher.HashPassword(password);
            if (user == null && user.PasswordHash != Passhash)
            {
                ViewBag.Message = "Invalid email or password.";
                return View();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };
            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("CookieAuth", principal);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Login");
        }
        public IActionResult AccessDenied() => View();
    }
}
