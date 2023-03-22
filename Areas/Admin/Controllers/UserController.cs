using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TDStore.Models;

namespace TDStore.Areas.Admin.Controllers;

[Area("Admin")]
public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<UserController> _logger;
    public UserController(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        SignInManager<ApplicationUser> signInManager,
        ILogger<UserController> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _logger = logger;
    }
    
    public async Task<IActionResult> CreateRole() {
        try{
            IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole() { Name = "Staff" });
            IdentityResult result2 = await _roleManager.CreateAsync(new ApplicationRole() { Name = "Customer" });
            if (result.Succeeded)
                    ViewBag.Message = "Role Created Successfully";
        } catch{
                    ViewBag.Message = "Error Create role";
        }
        return View();
    }

        
        public IActionResult CreateUser() {
            return View();
        }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUser([Required] string username, [Required] string firstname, [Required] string lastname, [Required] string email, [Required] string password,[Required] string role)
    {

        if (ModelState.IsValid)
        {
            ApplicationUser appUser = new ApplicationUser
            {
                UserName = username,
                FirstName = firstname,
                LastName = lastname,
                Email = email,
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, password);
            await _userManager.AddToRoleAsync(appUser, role);

            if (result.Succeeded)
                ViewBag.Message = "User Created Successfully";

            else
            {
                foreach (IdentityError error in result.Errors)
                     ViewBag.Message = error.Description;
            }
        }

        return View();
    }
}

