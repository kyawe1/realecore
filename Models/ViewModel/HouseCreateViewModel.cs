using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace RealEstateCore.Models.ViewModel;

public class HouseCreateViewModel{
    [Required]
    public string Name { set; get; }
    public string? Price { set; get; }
    public string? House_Type { set; get; }
    public string? type { set; get; }
    public int? number_of_bedroom { set; get; }
    public int? number_of_bathroom { set; get; }
    public string? Detail { set; get; }
    public string? Address { set; get; }
    [Required]
    [DataType(DataType.Upload)]
    public IFormFile CoverPic {set;get;}
    public string getEsapcedString(){
        return this.Name.Replace(" ","_");
    }
}