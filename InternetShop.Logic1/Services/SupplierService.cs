using InternetShop.Data.Models;
using InternetShop.Logic.Repository.Interfaces;
using InternetShop.Logic.Services.Interfaces;

namespace InternetShop.Logic.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task Create(Supplier supplier)
        {
            try
            {
                await _supplierRepository.Create(supplier);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(int? id)
        {
            try
            {   
                var supplier = await _supplierRepository.Get(id);
                if (supplier == null)
                    throw new NullReferenceException("Supplier was not found!");
                await _supplierRepository.Delete(supplier);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Supplier> Get(int? id)
        {
            try
            {
                return await _supplierRepository.Get(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Supplier>> GetAll()
        {
            try
            {
                return await _supplierRepository.GetAll();
            }
            catch (Exception)
            {
                return Enumerable.Empty<Supplier>();
            }
        }

        public async Task Update(Supplier supplier)
        {
            try
            {
                await _supplierRepository.Update(supplier);
            }
            catch (NullReferenceException)
            {
                throw;
            }
        }
    }
}
