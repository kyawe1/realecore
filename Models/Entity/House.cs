using System.ComponentModel.DataAnnotations;

namespace RealEstateCore.Models.Entity;


public class House
{
    [Key]
    public int Id { set; get; }
    [Required]
    public string Name { set; get; }
    public string? Price { set; get; }
    public string? House_Type { set; get; }
    public string? type { set; get; }
    public int? number_of_bedroom { set; get; }
    public int? number_of_bathroom { set; get; }
    public string? Detail { set; get; }
    public string? Address { set; get; }
    public string? path { set; get; }
    public virtual ICollection<Interest>? Interests { set; get; }

    public string getlink(){
        return $"/media/{path}";
    }
    
}