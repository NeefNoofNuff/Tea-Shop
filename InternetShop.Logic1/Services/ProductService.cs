using InternetShop.Data.Models;
using InternetShop.Logic.Repository;
using InternetShop.Logic.Repository.Interfaces;
using InternetShop.Logic.Services.Interfaces;

namespace InternetShop.Logic.Services
{
    public class ProductService : IProductService
    {
        private readonly IShoppingRepository _productRepository;

        public ProductService(IShoppingRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Create(Product product)
        {
            try
            {
                await _productRepository.Create(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task Delete(int? id)
        {
            try
            {
                var product = await Get(id);
                if (product == null)
                    throw new NullReferenceException("Product with this ID doesn't exist");
                await _productRepository.Delete(product);
            }
            catch (NullReferenceException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public Task<Product> Get(int? id)
        {
            try
            {
                return _productRepository.Get(id);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            try
            {
                return await _productRepository.GetAll();
            }
            catch (Exception)
            {
                return Enumerable.Empty<Product>();
            }
        }

        public async Task Update(Product product)
        {
            try
            {
                await _productRepository.Update(product);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
