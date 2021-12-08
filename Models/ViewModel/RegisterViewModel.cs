using System.ComponentModel.DataAnnotations;

namespace RealEstateCore.Models.ViewModel;

public class RegisterViewModel
{
    [Required]
    [EmailAddress]
    public string email { set; get; }
    [Required]
    [DataType(DataType.Password)]
    public string password { set; get; }
    [Required]
    [Compare("password")]
    [DataType(DataType.Password)]
    public string confirm_password { set; get; }
}