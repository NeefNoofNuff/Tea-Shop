using InternetShop.Logic.Repository.Interfaces;
using InternetShop.Data.Models;
using InternetShop.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Logic.Repository
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

        public async Task<bool> ReduceUnitStockAsync(ICollection<Product> products, double unit)
        {
            var newStock = new Dictionary<Product,double>();
            foreach (var product in products)
            {
                var newStockItem = product.UnitInStock - unit;
                if (newStockItem < 0)
                {
                    return false;
                }
                else
                {
                    newStock.Add(product,newStockItem);
                }
            }
            foreach (var pair in newStock)
            {
                pair.Key.UnitInStock = pair.Value;
                _shoppingContext.Update(pair.Key);
                await _shoppingContext.SaveChangesAsync();
            }
            return true;
        }

        public async Task Update(Product product)
        {
            _shoppingContext.Update(product);
            await _shoppingContext.SaveChangesAsync();
        }
    }
}
