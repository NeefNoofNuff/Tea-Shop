using InternetShop.Data.Models;
using InternetShop.Logic.Services;
using Microsoft.Data.SqlClient;

namespace InternetShop.Logic.Filtering.Interfaces
{
    public interface IEnumerableOrdering<T>
    {
        public IEnumerable<T> SortByName(IEnumerable<T> collection, string order = "ascending");
        public IEnumerable<T> SortById(IEnumerable<T> collection, string order = "ascending");
        public IEnumerable<T> SortByPrice(IEnumerable<T> collection, string order = "ascending");
        public IEnumerable<T> Sort(IEnumerable<T> collection, string sortOrder);
    }
}
