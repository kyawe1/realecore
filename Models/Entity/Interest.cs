using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace RealEstateCore.Models.Entity;

public class Interest {
    [Key]
    public int Id{set;get;}
    [ForeignKey("user")]
    public string UserId{set;get;}
    
    public ApplicationUser user{set;get;}
    [ForeignKey("house")]
    public int house_id{set;get;}
    public House house{set;get;}
    public bool inform{set;get;}=false;
    public DateTime Created_at{set;get;}

    public Interest(){
        Created_at=DateTime.UtcNow;
    }
    
}