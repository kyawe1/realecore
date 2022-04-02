using RealEstateCore.Data;
using RealEstateCore.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;
using RealEstateCore.Services;
using Microsoft.EntityFrameworkCore;
using RealEstateCore.Services.Doers;



var builder = WebApplication.CreateBuilder(args);

var version=new MySqlServerVersion(new Version(8,0,28));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DefaultDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultDbContext"),version));
builder.Services.AddDefaultIdentity<ApplicationUser>(options=>options.SignIn.RequireConfirmedAccount=false).AddRoles<IdentityRole>().AddEntityFrameworkStores<DefaultDbContext>();
builder.Services.AddSingleton<IFileStore,StoreFile>();
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}else{
    app.UseDeveloperExceptionPage();
}


app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
