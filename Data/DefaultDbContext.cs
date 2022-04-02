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
        string RoleId=Guid.NewGuid().ToString();
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole(){
                Id=RoleId,
                Name="Admin",
                NormalizedName="ADMIN"
            }
        );
        string userId=Guid.NewGuid().ToString();
        ApplicationUser user=new ApplicationUser{
            Id=userId,
            UserName="one@one.com",
            Email="one@one.com",
            SecurityStamp=Guid.NewGuid().ToString(),
            NormalizedEmail="ONE@ONE.COM",
            NormalizedUserName="ONE@ONE.COM"
        };
        IPasswordHasher<ApplicationUser> hasher=new PasswordHasher<ApplicationUser>();
        user.PasswordHash=hasher.HashPassword(user,"123456");
        builder.Entity<ApplicationUser>().HasData(user);
        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>(){
            RoleId=RoleId,
            UserId=userId
        });
    }
    public DbSet<Interest> Interests { set; get; }
    public DbSet<House> houses { set; get; }
}