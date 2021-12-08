using Microsoft.AspNetCore.Mvc;


namespace RealEstateCore.Controllers;

public class UploadController :Controller{
    public UploadController(){

    }
    [HttpGet]
    [Route("/media/{main}/{uploadPath}/{name}")]
    public IActionResult Index(string main,string uploadPath,string name){
        var file=System.IO.File.Open($"uploads/{main}/{uploadPath}/{name}",System.IO.FileMode.Open);
        return File(file,"image/png");
    }
}