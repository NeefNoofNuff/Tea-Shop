using InternetShop.Data.Models;
using InternetShop.Logic.Filtering.Interfaces;
using InternetShop.Logic.Repository.Interfaces;
using InternetShop.Logic.Services.Interfaces;

namespace InternetShop.Logic.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IEnumerableOrdering<Supplier> _sorting;
        private readonly IEnumerableFilter<Supplier> _filtering;

        public SupplierService(ISupplierRepository supplierRepository,
            IEnumerableOrdering<Supplier> sorting,
            IEnumerableFilter<Supplier> filtering)
        {
            _supplierRepository = supplierRepository;
            _sorting = sorting;
            _filtering = filtering;
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

        public async Task<IEnumerable<Supplier>> Sort(string sortOrder, string searchString)
        {
            var suppliers = await GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                suppliers = _filtering.Filtering(suppliers, 
                    supplier => supplier.CompanyName.Contains(searchString) 
                 || supplier.FirstName.Contains(searchString) 
                 || supplier.LastName.Contains(searchString));
            }

            suppliers = _sorting.Sort(suppliers, sortOrder);

            return suppliers;
        }
    }
}
