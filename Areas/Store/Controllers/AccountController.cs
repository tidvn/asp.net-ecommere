using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TDStore.Models;

namespace TDStore.Areas.Store.Controllers;
[Area("Store")]
public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        public AccountController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }
        public IActionResult Index()
        {
            return Redirect("/");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Required] string Username, [Required] string Password, string? returnurl = null)
        {
            if (ModelState.IsValid)
            {

                ApplicationUser AppUser = await _userManager.FindByNameAsync(Username);
                if (AppUser != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(AppUser, Password, false, false);



                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User {UserName} logged in at {Time}.",AppUser.UserName, DateTime.UtcNow);
                        return Redirect(returnurl ?? "/");
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(Username), "Login Failed: Invalid Username or Password");
                        return View();
                    }
                }

            }

            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register() {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register( [Required] string username, [Required] string firstname, [Required] string lastname, [Required] string email, [Required] string phone, [Required] string password)
        {
            
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser
                {
                    UserName = username,
                    FirstName = firstname,
                    LastName = lastname,
                    Email = email,
                    PhoneNumber= phone,
                };
                
                 IdentityResult result = await _userManager.CreateAsync(appUser, password);
                 await _userManager.AddToRoleAsync(appUser, "Customer");

                if (result.Succeeded)
                    ViewBag.Message = "User Created Successfully";

                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return Redirect("/");
        }
    }

