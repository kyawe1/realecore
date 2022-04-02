using System.ComponentModel.DataAnnotations;

namespace RealEstateCore.Models.ViewModel;

public class LoginViewModel{
    [Required]
    [EmailAddress]
    public string email {set;get;}
    [Required]
    [DataType(DataType.Password)]
    public string password{set;get;}
    public bool remember_me{set;get;}=false;
}