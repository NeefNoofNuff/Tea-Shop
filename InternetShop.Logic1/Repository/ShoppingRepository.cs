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
            try
            {
                _shoppingContext.Products.Add(product);
                await _shoppingContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
        }

        public async Task Delete(Product product)
        {
            try
            {
                _shoppingContext.Remove(product);
                await _shoppingContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch(OperationCanceledException) 
            {
                throw;
            }

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

        public async Task<Product> Get(int? id)
        {
            try
            {
                var product = await _shoppingContext.Products
                .Include(x => x.Supplier)
                .FirstOrDefaultAsync(prod => prod.Id == id);
                if (product == null)
                    throw new NullReferenceException("Product not found!");
                return product;
            }
            catch (NullReferenceException)
            {
                throw;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetAll() => await _shoppingContext.Products.Include(x => x.Supplier).ToListAsync();
        public IEnumerable<Supplier> GetAllSuppliers() => _shoppingContext.Suppliers.ToList();

        public bool ReduceUnitStockAsync(ICollection<OrderDetail> details)
        {
            if (details == null
                || !details.Select(x => x.Product).Any()
                || details.Where(x => x.Product == null).Any()
                || details.Where(x => x.Product.UnitInStock - x.Units < 0).Any())
            {
                return false;
            }
            foreach (var detail in details)
            {
                detail.Product.UnitInStock -= detail.Units;
                _shoppingContext.Update(detail.Product);
            }
            return true;
        }

        public async Task Update(Product product)
        {
            try
            {
                _shoppingContext.Update(product);
                await _shoppingContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
        }
    }
}
