using InternetShop.Data.Models;

namespace InternetShop.Logic.Services.Interfaces
{
    public interface IProductService
    {
        public Task Create(Product item);
        public Task Update(Product item);
        public Task<Product> Get(int? id);
        public Task<IEnumerable<Product>> GetAll();
        public Task Delete(int? id);
    }
}
