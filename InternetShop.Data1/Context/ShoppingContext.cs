using Microsoft.EntityFrameworkCore;
using InternetShop.Data.Models;

namespace InternetShop.Data.Context
{
    public class ShoppingContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<SuperMarket> SuperMarkets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public ShoppingContext(DbContextOptions<ShoppingContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Order>()
            //    .HasOne(order => order.Product)
            //    .WithMany(product => product.Orders)
            //    .HasForeignKey(key => key.ProductId);

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

            modelBuilder.Entity<Product>()
                 .HasOne(product => product.Supplier)
                 .WithMany(supplier => supplier.Products)
                 .HasForeignKey(product => product.SupplierId);

            modelBuilder.Entity<Order>()
                .HasMany(order => order.Products)
                .WithMany(product => product.Orders);

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
                    new Product
                    {
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
        }
    }
}
