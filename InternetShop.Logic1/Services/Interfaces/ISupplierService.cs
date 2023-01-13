using InternetShop.Data.Models;

namespace InternetShop.Logic.Services.Interfaces
{
    public interface ISupplierService
    {
        public Task Create(Supplier item);
        public Task Update(Supplier item);
        public Task<Supplier> Get(int? id);
        public Task<IEnumerable<Supplier>> GetAll();
        public Task Delete(int? id);
        public Task<IEnumerable<Supplier>> Sort(string sortOrder, string searchString);
    }
}
