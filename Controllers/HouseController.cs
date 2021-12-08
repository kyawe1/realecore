using Microsoft.AspNetCore.Mvc;
using RealEstateCore.Data;
using RealEstateCore.Models.Entity;
using RealEstateCore.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using RealEstateCore.Services;


namespace RealEstateCore.Controllers;

public class HouseController : Controller
{
    private readonly DefaultDbContext _context;
    private readonly IFileStore _fileStore;
    public HouseController(DefaultDbContext context,IFileStore fileStore)
    {
        _context = context;
        _fileStore=fileStore;
    }
    [Authorize(Roles="Admin")]
    public IActionResult Index()
    {
        return View(_context.houses.ToList());
    }
    [HttpGet]
    [Authorize(Roles="Admin")]
    public IActionResult Create()
    {
        return View();
    }
    [HttpGet]
    [Authorize(Roles="Admin")]
    public IActionResult Update(int id)
    {
        var model=_context.houses.Find(id);
        return View(model);
    }
    [HttpPost]
    [Authorize(Roles="Admin")]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind(include: new String[]{
        "Name",
        "Price",
        "House_Type",
        "type",
        "number_of_bedroom",
        "number_of_bathroom",
        "Detail",
        "Address",
        "CoverPic"
    })] HouseCreateViewModel house)
    {
        if (!ModelState.IsValid)
        {
            return View(house);
        }
        try
        {
            string location=$"houses/{house.getEsapcedString()}/{Convert.ToString((Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds)}-{house.getEsapcedString()}.png";
            var path=_fileStore.Save(house.CoverPic,location,house.getEsapcedString());
            House h=new House(){
                Name=house.Name,
                Price=house.Price,
                House_Type=house.House_Type,
                type=house.type,
                number_of_bathroom=house.number_of_bathroom,
                number_of_bedroom=house.number_of_bedroom,
                Detail=house.Detail,
                Address=house.Address,
                path=location
            };
            _context.houses.Add(h);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            Console.Write("theres a exception");
        }

        return View(house);
    }
    [HttpPost]
    [Authorize(Roles="Admin")]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int id,[Bind(include: new String[]{
        "Name",
        "Price",
        "House_Type",
        "type",
        "number_of_bedroom",
        "number_of_bathroom",
        "Detail",
        "Address"
    })] House house)
    {
        if(! ModelState.IsValid){
            return View(house);
        }
        _context.Update(house); 
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    [Authorize(Roles="Admin")]
    public IActionResult Delete(int id)
    {
        var model=_context.houses.Find(id);
        _context.houses.Remove(model);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}