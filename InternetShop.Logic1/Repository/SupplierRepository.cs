using InternetShop.Logic.Repository.Interfaces;
using InternetShop.Data.Context;
using InternetShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Logic.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ShoppingContext _shoppingContext;
        public SupplierRepository(ShoppingContext shoppingContext)
        {
            _shoppingContext = shoppingContext;
        }

        public async Task Create(Supplier supplier)
        {
            try
            {
                _shoppingContext.Suppliers.Add(supplier);
                await _shoppingContext.SaveChangesAsync();
            }
            catch (ArgumentNullException)
            {
                throw;
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

        public async Task Delete(Supplier supplier)
        {
            try
            {
                var productsWithSupplier = _shoppingContext.Products
                    .Where(product => product.SupplierId == supplier.Id)
                    .ToList();
                foreach (var product in productsWithSupplier)
                {
                    _shoppingContext.Products.Remove(product);
                }

                _shoppingContext.Suppliers.Remove(supplier);
                await _shoppingContext.SaveChangesAsync();
            }
            catch (ArgumentNullException)
            {
                throw;
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

        public async Task<Supplier> Get(int? id)
        {
            try
            {
                var supplier = await _shoppingContext.Suppliers.FirstOrDefaultAsync(supp => supp.Id == id);
                if (supplier == null)
                {
                    throw new NullReferenceException("ID in supplier is null");
                }
                return supplier;
            }
            catch(OperationCanceledException)
            {
                throw;
            }
            catch (NullReferenceException)
            {
                throw;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Supplier>> GetAll()
        {
            try
            {
                return await _shoppingContext.Suppliers.ToListAsync();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch(OperationCanceledException)
            {
                throw;
            }

        }

        public async Task Update(Supplier supplier)
        {
            try
            {
                _shoppingContext.Update(supplier);
                await _shoppingContext.SaveChangesAsync();
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
    }
}
