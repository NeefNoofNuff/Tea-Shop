
using InternetShop.Data.Models;
using InternetShop.Logic.Services.Interfaces;
using System.Linq;
using X.PagedList;

namespace InternetShop.Logic.Services
{
    public class PagingTools : IPaging
    {
        public const int ElementsPerPage = 5;
        public const string SortingByNameDesc = "name_desc";
        public const string SortingByPrice = "Price";
        public const string SortingByPriceDesc = "price_desc";

        public readonly IProductService _productService;
        public readonly ISupplierService _supplierService;

        public PagingTools(IProductService productService, ISupplierService supplierService)
        {
            _productService = productService;
            _supplierService = supplierService;
        }

        public async Task<IPagedList<Product>> PageViewProductAsync(string sortOrder, string searchString, int? page)
        {
            var products = await _productService.Sort(sortOrder, searchString);
            int pageNumber = (page ?? 1);
            var currentPage = products.Skip((pageNumber - 1) * ElementsPerPage).Take(ElementsPerPage);

            return products.ToPagedList(pageNumber, ElementsPerPage);
        }
        public async Task<IPagedList<Supplier>> PageViewSupplierAsync(string sortOrder, string searchString, int? page)
        {
            var products = await _supplierService.Sort(sortOrder, searchString);
            int pageNumber = (page ?? 1);
            return products.ToPagedList(pageNumber, ElementsPerPage);
        }
    }
}
