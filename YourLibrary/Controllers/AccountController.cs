using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YourLibrary.Data;
using YourLibrary.Enums;
using YourLibrary.Models;
using YourLibrary.ViewModels;

namespace YourLibrary.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser>   _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    // GET
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    //POST
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
        {
            // Model state is not valid
            return View(loginViewModel);
        }

        var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

        if (user != null)
        {
            // User is found. Password checking
            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

            if (passwordCheck)
            {
                // Password is correct. Trying to sign in
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                if (result.Succeeded)
                {
                    // User has successfully signed in
                    return RedirectToAction("Index", "Books");
                }
            }
             
            // Password is incorrect
            TempData["Error"] = "Wrong password";
            return View(loginViewModel);
        }

        //user not found
        TempData["Error"] = "Wrong credentials";
        
        return View(loginViewModel);
    }
    
    // GET
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    
    //POST
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid) return View(registerViewModel);

        var user = await _userManager.FindByEmailAsync(registerViewModel.Email);
        
        // If we have a user
        if (user != null)
        {
            TempData["Error"] = "This email address is already in use";

            return View(registerViewModel);
        }

        var newUser = new AppUser
        {
            UserName = registerViewModel.Email,
            Email = registerViewModel.Email,
            Role = UserRole.Reader
        };
        var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

        
        if (!await _roleManager.RoleExistsAsync(UserRole.Reader.ToString()))
            await _roleManager.CreateAsync(new IdentityRole(UserRole.Reader.ToString()));

        if (!newUserResponse.Succeeded)
        {
            var roleResult = await _userManager.AddToRoleAsync(newUser, UserRole.Reader.ToString());

            if(roleResult.Succeeded) return View("Error");
        }

        return View("~/Views/Books/Index.cshtml");
    }

    [HttpPost]
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }
}