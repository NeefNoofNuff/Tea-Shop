using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using InternetShop.Data.Context;

namespace InternetShopTesting
{
    public class ShoppingDbIntegraionTests : IDisposable
    {
        ShoppingContext _context;

        public ShoppingDbIntegraionTests()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ShoppingContext>();

            builder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=shopping_db_{Guid.NewGuid()};Trusted_Connection=True;MultipleActiveResultSets=true")
                    .UseInternalServiceProvider(serviceProvider);

            _context = new ShoppingContext(builder.Options);
            _context.Database.Migrate();

        }

        [Fact]
        public void QueryProductsFromSqlTest()
        {
            //Execute the query
            ShoppingQuery query = new ShoppingQuery(_context);
            var products = query.ExecuteProduct();
            Assert.Equal(10, products.Count());
            var testProduct = products.First();
            Assert.Equal("Eceri Green Georgian Tea", testProduct.Name);
            Assert.Equal("200", testProduct.UnitInStock.ToString());
        }

        [Fact]
        public void QuerySuppliersFromSqlTest()
        {
            //Execute the query
            ShoppingQuery query = new ShoppingQuery(_context);
            var suppliers = query.ExecuteSuppliers();

            //Verify the results
            Assert.Equal(7, suppliers.Count());
            var supplier = suppliers.Where(supp => supp.Id == 5).Single();
            Assert.Equal("Edward", supplier.FirstName);
        }


        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
