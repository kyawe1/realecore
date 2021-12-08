using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using RealEstateCore.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using RealEstateCore.Models.Entity;
using System.Threading.Tasks;


namespace RealEstateCore.Controllers;

public class IdentityController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl=null)
    {
        if(returnUrl!=null){

        }
        return View();
    }
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([Bind("email,password,remember_me")] LoginViewModel model,string? returnUrl)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var result = await _signInManager.PasswordSignInAsync(model.email, model.password, false, false);
        if (result.Succeeded)
        {
            return  returnUrl !=null ? Redirect(returnUrl) :RedirectToAction(nameof(Index), "Home");
        }
        ModelState.AddModelError("error","UserNotFound");
        return View(model);
    }
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([Bind("email,password,confirm_password")] RegisterViewModel model,string? returnUrl)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var user=new ApplicationUser(){
            UserName=model.email,
            Email=model.email,
        };
        var result = await _userManager.CreateAsync(user,model.password);
        var m=await _userManager.FindByEmailAsync(user.Email);
        if (result.Succeeded)
        {
            var code =await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return  RedirectToAction(nameof(Index), "Home");
        }
        ModelState.AddModelError("error","UserNotFound");
        return View(model);
    }
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Logout(){
        await _signInManager.SignOutAsync();
        TempData["signal"]="You Have Been Logout";
        return  RedirectToAction(nameof(Index), "Home");
    }
    [HttpGet]
    [Route("admin/register")]
    public async Task<IActionResult> AdminRegister()
    {
       return View();
    }
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [Route("admin/register")]
    public async Task<IActionResult> AdminRegister([Bind("email,password,confirm_password")] RegisterViewModel model,string? returnUrl)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var user=new ApplicationUser(){
            UserName=model.email,
            Email=model.email,
        };
        var result = await _userManager.CreateAsync(user,model.password);
        var m=await _userManager.FindByEmailAsync(user.Email);
        
        var result1=await _userManager.AddToRoleAsync(user,"Admin");
        if (result.Succeeded && result1.Succeeded)
        {
            var code =await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return  RedirectToAction(nameof(Index), "Home");
        }
        ModelState.AddModelError("error","UserNotFound");
        return View(model);
    }
}
