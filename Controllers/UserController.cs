using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RealEstateCore.Data;
using RealEstateCore.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RealEstateCore.Controllers;


public class UserController : Controller
{
    private readonly DefaultDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public UserController(DefaultDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _context = dbContext;
        _userManager = userManager;
    }
    [Authorize]
    public IActionResult Interest()
    {
        ICollection<Interest> list=_context.Interests.Include(p=>p.house).ToList();
        return View(list);
    }
    [Authorize]
    public async Task<IActionResult> Add(int id)
    {
        var q = await _userManager.FindByNameAsync(User.Identity.Name);
        var model = _context.houses.Find(id);
        Interest? i;
        if (model != null)
        {
            i = new Interest()
            {
                UserId = q.Id,
                house_id = model.Id
            };
            _context.Interests.Add(i);
            _context.SaveChanges();
        }
        string referer=Request.Headers["Referer"].ToString();
        return Redirect(referer);
    }
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var temp=await _context.Interests.FindAsync(id);
        var user=await _userManager.FindByNameAsync(User.Identity.Name);
        if(user.Id==temp.UserId){
            _context.Remove(temp);
            _context.SaveChanges();
            TempData["good"]="Your object is Deleted";
            return RedirectToAction(nameof(Interest));
        }
        return NotFound();
    }
    public IActionResult Profile(){
        return View();
    }
}