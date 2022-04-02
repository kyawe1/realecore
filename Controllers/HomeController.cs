using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RealEstateCore.Models.Entity;
using RealEstateCore.Data;
using RealEstateCore.Models.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace RealEstateCore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DefaultDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public HomeController(ILogger<HomeController> logger, DefaultDbContext context, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> All()
    {
        if (User.Identity.IsAuthenticated)
        {
            List<HouseDetailViewModel> mo = new List<HouseDetailViewModel>();
            var qo = _context.houses.ToList();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            foreach (var i in qo)
            {
                var n = _context.Interests.Where(p => p.house_id == i.Id && p.UserId == user.Id);
                if (n.Count() != 0)
                {
                    mo.Add(new HouseDetailViewModel()
                    {
                        house = i,
                        is_interested = true
                    });
                }
                else
                {
                    mo.Add(new HouseDetailViewModel()
                    {
                        house = i,
                        is_interested = false
                    });
                }
            }
            return View(mo);
        }
        List<HouseDetailViewModel> m = new List<HouseDetailViewModel>();
        var q = _context.houses.ToList();
        foreach (var i in q)
        {
            m.Add(new HouseDetailViewModel()
            {
                house = i,
                is_interested = false
            });
        }
        return View(m);

    }
    public async Task<IActionResult> Detail(int id)
    {
        HouseDetailViewModel? b = null;
        var q = _context.houses.Find(id);
        if (q == null)
        {
            return NotFound();
        }
        bool n = false;
        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var ok = _context.Interests.Where(i => i.house_id == id && i.UserId == user.Id);

            if (ok.Count() != 0)
            {
                n = true;
            }

            if (ok != null && q != null)
            {
                b = new HouseDetailViewModel()
                {
                    house = q,
                    is_interested = n
                };
            }
        }
        b = new HouseDetailViewModel()
        {
            house = q,
            is_interested = false
        };
        return View(b);
    }
    public async Task<IActionResult> Search(String? id, string? search)
    {
        List<HouseDetailViewModel> mo = new List<HouseDetailViewModel>();
        IEnumerable<House> q;

        if (id != null)
        {
            q = from i in _context.houses.ToList()
                where i.type == id
                select i;
        }
        else
        {
            q = from i in _context.houses.ToList()
                where i.Name == search
                select i;
        }
        foreach (var k in q)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var n = _context.Interests.Where(i => i.house_id == k.Id && i.UserId == user.Id);
            if (User.Identity.IsAuthenticated)
            {

                if (n.Count() != 0)
                {
                    mo.Add(new HouseDetailViewModel()
                    {
                        house = k,
                        is_interested = true
                    });
                }
                else
                {
                    mo.Add(new HouseDetailViewModel()
                    {
                        house = k,
                        is_interested = false
                    });
                }
            }
            else
            {
                mo.Add(new HouseDetailViewModel()
                {
                    house = k,
                    is_interested = false
                });
            }



        }
        
        return View("~/Views/Home/All.cshtml", mo);
    }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
