using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InternetShop.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            if (!Roles.Any())
            {
                Roles.Add(new IdentityRole("Administrator"));
                Roles.Add(new IdentityRole("Accountant"));
                Roles.Add(new IdentityRole("RegularCustomer"));
                Roles.Add(new IdentityRole("SilverCustomer"));
                Roles.Add(new IdentityRole("GoldenCustomer"));
                Roles.Add(new IdentityRole("Employee"));
            }
        }
    }
}