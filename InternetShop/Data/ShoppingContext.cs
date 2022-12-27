using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TaskAuthenticationAuthorization.Models;


namespace InternetShop.Data
{
    public class ShoppingContext : DbContext
    {
        public const string ADMIN_ROLE_NAME = "admin";
        public const string BUYER_ROLE_NAME = "buyer";


        public DbSet<Product> Products { get; set; }
        // public DbSet<Customer> Customers { get; set; }
        //  public DbSet<SuperMarket> SuperMarkets { get; set; }
        // public DbSet<Order> Orders { get; set; }
        //  public DbSet<OrderDetail> OrderDetails { get; set; }
        //  public DbSet<User> Users { get; set; }
        //  public DbSet<Role> Roles { get; set; }

        public ShoppingContext(DbContextOptions<ShoppingContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //    .HasOne(user => user.Role)
            //    .WithMany(role => role.Users)
            //    .HasForeignKey(user => user.RoleId);

            //Initialize(modelBuilder);

            //base.OnModelCreating(modelBuilder);
        }

        private static void Initialize(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>().HasData(
            //    new Product
            //    {
            //        Id = 1,
            //        Name = "Butter",
            //        Price = 30.0
            //    },
            //    new Product
            //    {
            //        Id = 2,
            //        Name = "Banana",
            //        Price = 20.50
            //    },
            //    new Product
            //    {
            //        Id = 3,
            //        Name = "Cola",
            //        Price = 9.30
            //    }
            //);
            //modelBuilder.Entity<Customer>().HasData(
            //    new Customer
            //    {
            //        Id = 1,
            //        FirstName = "Ostap",
            //        LastName = "Bender",
            //        Address = "Rio de Zhmerinka",
            //        Discount = Discount.O,
            //        UserId = 2,
            //    },
            //    new Customer
            //    {
            //        Id = 2,
            //        FirstName = "Shura",
            //        LastName = "Balaganov",
            //        Address = "Odessa",
            //        Discount = Discount.R,
            //        UserId = 3,
            //    }
            //);
            //modelBuilder.Entity<SuperMarket>().HasData(
            //    new SuperMarket
            //    {
            //        Id = 1,
            //        Name = "Wellmart",
            //        Address = "Lviv",

            //    },
            //    new SuperMarket
            //    {
            //        Id = 2,
            //        Name = "Billa",
            //        Address = "Odessa",

            //    }
            //);
            //modelBuilder.Entity<Order>().HasData(
            //    new Order
            //    {
            //        Id = 1,
            //        CustomerId = 1,
            //        SuperMarketId = 1,
            //        OrderDate = DateTime.Now,
            //    },
            //    new Order
            //    {
            //        Id = 2,
            //        CustomerId = 1,
            //        SuperMarketId = 1,
            //        OrderDate = DateTime.Now,
            //    }, 
            //    new Order
            //    {
            //        Id = 3,
            //        CustomerId = 2,
            //        SuperMarketId = 2,
            //        OrderDate = DateTime.Now,
            //    }
            //);
            //modelBuilder.Entity<OrderDetail>().HasData(
            //    new OrderDetail
            //    {
            //        Id = 1,
            //        OrderId = 1,
            //        ProductId = 1,
            //        Quantity = 2

            //    },
            //    new OrderDetail
            //    {
            //        Id = 2,
            //        OrderId = 2,
            //        ProductId = 2,
            //        Quantity = 1
            //    }
            //);
            //string adminLogin = "admin";
            //string adminPassword = "123456";

            //string buyerRegularLogin = "regular";
            //string buyerRegularPassword = "123456";

            //string buyerWholesaleLogin = "wholesale";
            //string buyerWholesalePassword = "123456";

            //string buyerGoldenLogin = "golden";
            //string buyerGoldenPassword = "123456";

            //// добавляем роли
            //Role adminRole = new Role {Id = 1, Name = ADMIN_ROLE_NAME};
            //Role buyerRole = new Role {Id = 2, Name = BUYER_ROLE_NAME};
            //User adminUser = new User {Id = 1, Login = adminLogin, Password = adminPassword, RoleId = adminRole.Id};
            //User buyerRegularUser = new User
            //{
            //    Id = 2,
            //    Login = buyerRegularLogin,
            //    Password = buyerRegularPassword,
            //    RoleId = buyerRole.Id,
            //    BuyerType = BuyerType.Regular
            //};
            //User buyerWholesaleUser = new User
            //{
            //    Id = 3,
            //    Login = buyerWholesaleLogin,
            //    Password = buyerWholesalePassword,
            //    RoleId = buyerRole.Id,
            //    BuyerType = BuyerType.Wholesale
            //};
            //User buyerGoldenUser = new User
            //{
            //    Id = 4,
            //    Login = buyerGoldenLogin,
            //    Password = buyerGoldenPassword,
            //    RoleId = buyerRole.Id,
            //    BuyerType = BuyerType.Golden
            //};


            //modelBuilder.Entity<Role>().HasData(adminRole, buyerRole);
            //modelBuilder.Entity<User>().HasData(adminUser, buyerRegularUser, buyerGoldenUser, buyerWholesaleUser);

        }
    }
}
