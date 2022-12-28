using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TaskAuthenticationAuthorization.Models;
using InternetShop.Models;

namespace InternetShop.Data
{
    public class ShoppingContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<SuperMarket> SuperMarkets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public ShoppingContext(DbContextOptions<ShoppingContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(order => order.OrderDetails)
                .WithOne(detail => detail.Order)
                .HasForeignKey(key => key.OrderId);

            modelBuilder.Entity<Supplier>()
                .Property(prop => prop.FirstName)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<Supplier>()
                .Property(prop => prop.LastName)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<Supplier>()
                .Property(prop => prop.CompanyName)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<Supplier>()
                .Property(prop => prop.PhoneNumber)
                .IsRequired();

            modelBuilder.Entity<OrderDetail>()
                .Property(prop => prop.Quantity)
                .IsRequired();

            modelBuilder.Entity<OrderDetail>()
                .HasOne(detail => detail.Product)
                .WithMany(products => products.OrderDetails)
                .HasForeignKey(detail => detail.ProductId);

            modelBuilder.Entity<Product>()
                 .HasOne(product => product.Supplier)
                 .WithMany(supplier => supplier.Products)
                 .HasForeignKey(product => product.SupplierId);

            Initialize(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void Initialize(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supplier>().HasData(
                    new Supplier
                    {
                        Id = 1,
                        FirstName = "Gregory",
                        LastName = "Mcmillan",
                        CompanyName = "Magical Tea",
                        PhoneNumber = "0512223192"
                    },
                    new Supplier
                    {
                        Id = 2,
                        FirstName = "Alice",
                        LastName = "Scott",
                        CompanyName = "Awesome Tea",
                        PhoneNumber = "057376629"
                    },
                    new Supplier
                    {
                        Id = 3,
                        FirstName = "Tony",
                        LastName = "Weiss",
                        CompanyName = "Futuristic Tea",
                        PhoneNumber = "057456629"
                    },
                    new Supplier
                    {
                        Id = 4,
                        FirstName = "Joseph",
                        LastName = "Beard",
                        CompanyName = "Creatively Tea",
                        PhoneNumber = "057345233"
                    },
                    new Supplier
                    {
                        Id = 5,
                        FirstName = "Edward",
                        LastName = "Ruiz",
                        CompanyName = "My Tea",
                        PhoneNumber = "0971523684"
                    },
                    new Supplier
                    {
                        Id = 6,
                        FirstName = "Cecilia",
                        LastName = "Conway",
                        CompanyName = "Holy Tea",
                        PhoneNumber = "0935412687"
                    },
                    new Supplier
                    {
                        Id = 7,
                        FirstName = "Herbie",
                        LastName = "Haley",
                        CompanyName = "Tea Side",
                        PhoneNumber = "0964512756"
                    }
                );

            modelBuilder.Entity<Product>().HasData(
                    new Product {
                        Id = 817,
                        Name = "Shen Puer Tea «Lapis Dragon»",
                        Price = 223,
                        UnitInStock = 50.0,
                        SupplierId = 1
                    },
                    new Product
                    {
                        Id = 560,
                        Name = "Garnet Black Tea",
                        Price = 86.85,
                        UnitInStock = 150.0,
                        SupplierId = 1
                    },
                    new Product
                    {
                        Id = 127,
                        Name = "Eceri Green Georgian Tea",
                        Price = 93.00,
                        UnitInStock = 200.0,
                        SupplierId = 2
                    },
                    new Product
                    {
                        Id = 715,
                        Name = "NonMatch Tea",
                        Price = 375.00,
                        UnitInStock = 25.0,
                        SupplierId = 2
                    },
                    new Product
                    {
                        Id = 300,
                        Name = "English Breakfast Tea",
                        Price = 65.65,
                        UnitInStock = 1000.0,
                        SupplierId = 3
                    },
                    new Product
                    {
                        Id = 204,
                        Name = "Milk OLoong Tea",
                        Price = 111.85,
                        UnitInStock = 50.0,
                        SupplierId = 4
                    },
                    new Product
                    {
                        Id = 700,
                        Name = "Alpen Plains Tea",
                        Price = 86.85,
                        UnitInStock = 10.0,
                        SupplierId = 5
                    },
                    new Product
                    {
                        Id = 534,
                        Name = "Royal Berghamot Tea",
                        Price = 118.35,
                        UnitInStock = 5.0,
                        SupplierId = 6
                    },
                    new Product
                    {
                        Id = 206,
                        Name = "Sky blue Tegu Tea",
                        Price = 105.00,
                        UnitInStock = 110.0,
                        SupplierId = 6
                    },
                    new Product
                    {
                        Id = 502,
                        Name = "Masala Tea",
                        Price = 86.85,
                        UnitInStock = 50.0,
                        SupplierId = 7
                    }
                );

            

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
