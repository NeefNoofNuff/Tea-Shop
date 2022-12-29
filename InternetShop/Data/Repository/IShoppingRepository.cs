using InternetShop.Models;
using Microsoft.AspNetCore.Mvc;
using InternetShop.Models;

namespace InternetShop.Data.Repository
{
    public interface IShoppingRepository
    {
        public Task Create(Product product);
        public Task<Product> Get(int? id);
        public Task Update(Product product);
        public Task Delete(Product product);

        public Task<IEnumerable<Product>> GetAll();
        public IEnumerable<Supplier> GetAllSuppliers();
        public bool Exist(int id);
    }
}
