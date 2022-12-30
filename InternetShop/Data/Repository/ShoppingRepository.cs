using InternetShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Data.Repository
{
    public class ShoppingRepository : IShoppingRepository
    {   
        private readonly ShoppingContext _shoppingContext;

        public ShoppingRepository(ShoppingContext shoppingContext)
        {
            _shoppingContext = shoppingContext;
        }

        public async Task Create(Product product)
        {   
            _shoppingContext.Products.Add(product);
            await _shoppingContext.SaveChangesAsync();
        }

        public async Task Delete(Product product)
        {
            _shoppingContext.Remove(product);
            await _shoppingContext.SaveChangesAsync();
        }

        public bool Exist(int id)
        {
            try
            {
                var result = Get(id);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public Task<Product> Get(int? id)
        {   
            var product = _shoppingContext.Products.FirstOrDefault(prod => prod.Id == id);
            if(product == null)
            {
                throw new NullReferenceException("Product not found!");
            }
            return Task.FromResult(product);
        }

        public async Task<IEnumerable<Product>> GetAll() => await _shoppingContext.Products.ToListAsync();

        public IEnumerable<Supplier> GetAllSuppliers() => _shoppingContext.Suppliers.ToList();

        public async Task<bool> ReduceUnitStockAsync(Product product, double unit)
        {
            var newStock = product.UnitInStock - unit;
            if (newStock < 0)
            {
                return false;
            }
            else
            {
                product.UnitInStock = newStock;
                _shoppingContext.Update(product);
                await _shoppingContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task Update(Product product)
        {
            _shoppingContext.Update(product);
            await _shoppingContext.SaveChangesAsync();
        }
    }
}
