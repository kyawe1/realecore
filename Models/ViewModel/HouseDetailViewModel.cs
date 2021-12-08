using System.ComponentModel.DataAnnotations;
using RealEstateCore.Models.Entity;
namespace RealEstateCore.Models.ViewModel;


public class HouseDetailViewModel{
    public House house {set;get;}
    public bool is_interested{set;get;}
}