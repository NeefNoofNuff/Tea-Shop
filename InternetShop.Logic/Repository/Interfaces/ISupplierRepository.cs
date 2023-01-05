using InternetShop.Models;

namespace InternetShop.Logic.Repository.Interfaces
{
    public interface ISupplierRepository
    {
        public Task Create(Supplier supplier);
        public Task<Supplier> Get(int? id);
        public Task Update(Supplier supplier);
        public Task Delete(Supplier supplier);
        public Task<IEnumerable<Supplier>> GetAll();
    }
}
