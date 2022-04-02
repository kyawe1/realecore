using Microsoft.AspNetCore.Mvc;

namespace RealEstateCore.Controllers;


public class AdminController : Controller{
    public IActionResult Index(){
        return View();
    }
}