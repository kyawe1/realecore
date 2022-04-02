using RealEstateCore.Services;


namespace RealEstateCore.Services.Doers;

public class StoreFile : IFileStore
{
    public string? Save(IFormFile file, string location,string name)
    {
        var filepath=Path.GetTempFileName();
        if(!Directory.Exists($"uploads/houses/{name}"))
        Directory.CreateDirectory($"uploads/houses/{name}");
        using(var stream=System.IO.File.Open($"uploads/{location}",System.IO.FileMode.OpenOrCreate)){
            file.CopyTo(stream);
        }
        return $"{location}";
    }
}