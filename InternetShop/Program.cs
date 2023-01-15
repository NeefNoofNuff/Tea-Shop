using InternetShop.Data.Context;
using InternetShop.Logic.Repository;
using InternetShop.Logic.Repository.Interfaces;
using InternetShop.Logic.Services;
using InternetShop.Logic.Services.Interfaces;
using InternetShop.Logic.Filtering;
using InternetShop.Logic.Filtering.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;
using System.Globalization;
using InternetShop.Data.Models;
using InternetShop.Presentation.Filters.Exceptions;

var builder = WebApplication.CreateBuilder(args);

var connectionAuthorizationString = builder.Configuration.GetConnectionString("AuthorizationConnection");
var shopContextConnectionString = builder.Configuration.GetConnectionString("ShopConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionAuthorizationString));

builder.Services.AddDbContext<ShoppingContext>(option => 
    option.UseSqlServer(shopContextConnectionString, b => b.MigrationsAssembly("InternetShop.Presentation")));

System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo("en-US");
customCulture.NumberFormat.NumberDecimalSeparator = ".";

CultureInfo.DefaultThreadCurrentCulture = customCulture;
CultureInfo.DefaultThreadCurrentUICulture = customCulture;

builder.Services.AddScoped<IShoppingRepository, ShoppingRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();

builder.Services.AddScoped<IMapRepository, MapRepository>();
builder.Services.AddScoped<IMapService, MapService>();

builder.Services.AddScoped<IEnumerableFilter<Product>, ProductCollectionFilter>();
builder.Services.AddScoped<IEnumerableFilter<Supplier>, SupplierCollectionFilter>();
builder.Services.AddScoped<IEnumerableOrdering<Product>, ProductCollectionOrdering>();
builder.Services.AddScoped<IEnumerableOrdering<Supplier>, SupplierCollectionOrdering>();

builder.Services.AddControllers(options =>
    { 
        //options.Filters.Add(new ExceptionFilterAttribute());
        options.Filters.Add(new DbConnectionExceptionAttribute());
    });

builder.Services.AddSingleton
    (GraphDatabase.Driver(
        "bolt://localhost:7687", 
        AuthTokens.Basic("neo4j", "map4nnn")
        )
    );

builder.Services.AddScoped<MapContext>();
builder.Services.AddScoped<InvoiceContext>();
builder.Services.AddScoped<PagingTools>();

builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();

builder.Services.AddScoped<InvoiceFactory>();

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
    options.AddPolicy("SupplierAccess",
        builder => builder.RequireRole("Administrator", "Employee"));
    options.AddPolicy("WriteAccess"
        , builder => builder.RequireRole("Administrator"));
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
public partial class Program { }