using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace RealEstateCore.Controllers;

class InterestController : Controller
{
    [Authorize(Roles="Admin")]
    public IActionResult Index()
    {
        return View();
    }
}