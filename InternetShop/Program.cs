using InternetShop.Controllers;
using InternetShop.Data;
using InternetShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionAuthorizationString = builder.Configuration.GetConnectionString("AuthorizationConnection");
var shopContextConnectionString = builder.Configuration.GetConnectionString("ShopConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionAuthorizationString));

builder.Services.AddDbContext<ShoppingContext>(option => 
    option.UseSqlServer(shopContextConnectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddAuthorization(options => {
    options.AddPolicy("RoleAccess",
        builder => builder.RequireRole("Administrator"));
    options.AddPolicy("ProductsBaseAccess"
        , builder => builder.RequireRole("Administrator", "Accountant", "Employee"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
