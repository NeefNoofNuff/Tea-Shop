using InternetShop.Data.Models;
using X.PagedList;

namespace InternetShop.Logic.Services.Interfaces
{
	public interface IPaging
	{
        public Task<IPagedList<Product>> PageViewProductAsync(string sortOrder, string searchString, int? page);
        public Task<IPagedList<Supplier>> PageViewSupplierAsync(string sortOrder, string searchString, int? page);
    }
}
