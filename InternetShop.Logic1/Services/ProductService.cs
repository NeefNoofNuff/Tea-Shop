using InternetShop.Data.Models;
using InternetShop.Logic.Filtering.Interfaces;
using InternetShop.Logic.Repository.Interfaces;
using InternetShop.Logic.Services.Interfaces;

namespace InternetShop.Logic.Services
{
    public class ProductService : IProductService
    {
        private readonly IShoppingRepository _productRepository;
        private readonly IEnumerableOrdering<Product> _sorting;
        private readonly IEnumerableFilter<Product> _filtering;

        public ProductService(IShoppingRepository productRepository, 
            IEnumerableOrdering<Product> sorting,
            IEnumerableFilter<Product> filtering)
        {
            _productRepository = productRepository;
            _sorting = sorting;
            _filtering = filtering;
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
                return  _productRepository.Get(id);
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

        public async Task<IEnumerable<Product>> Sort(string sortOrder, string searchString)
        {
            var products = await GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = _filtering.Filtering(products, product => product.Name.Contains(searchString, StringComparison.InvariantCultureIgnoreCase));
            }

            products = _sorting.Sort(products, sortOrder);

            return products;
        }

    }
}
