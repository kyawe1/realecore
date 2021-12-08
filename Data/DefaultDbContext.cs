using Microsoft.EntityFrameworkCore;
using RealEstateCore.Models.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace RealEstateCore.Data;


public class DefaultDbContext : IdentityDbContext<ApplicationUser>
{
    public DefaultDbContext(DbContextOptions<DefaultDbContext> option) : base(option)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole(){
                Name="Admin",
                NormalizedName="ADMIN"
            }
        );
    }
    public DbSet<Interest> Interests { set; get; }
    public DbSet<House> houses { set; get; }
}