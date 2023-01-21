using InternetShop.Data.Models;
using InternetShop.Logic.Filtering.Interfaces;
using InternetShop.Logic.Services;

namespace InternetShop.Logic.Filtering
{
    public class SupplierCollectionOrdering : IEnumerableOrdering<Supplier>
    {
        public IEnumerable<Supplier> Sort(IEnumerable<Supplier> collection, string sortOrder)
        {
            try
            {
                if (collection == null)
                    throw new ArgumentNullException(nameof(collection));
                switch (sortOrder)
                {
                    case PagingTools.SortingByNameDesc:
                        collection = SortByName(collection, "descending");
                        break;
                    case PagingTools.SortingByPrice:
                        collection = SortByPrice(collection);
                        break;
                    case PagingTools.SortingByPriceDesc:
                        collection = SortByPrice(collection, "descending");
                        break;
                    default:
                        collection = SortByName(collection);
                        break;
                }
                return collection;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Sorting by Id for any collection
        /// </summary>
        /// <param name="collection">Collection for sorting</param>
        /// <param name="order">Write descenting for sorting in desc order</param>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<Supplier> SortById(IEnumerable<Supplier> collection, string order = "ascending")
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (order == "descending")
            {
                collection = collection.OrderByDescending(x => x.Id).ToList();
            }
            else
            {
                collection = collection.OrderBy(x => x.Id).ToList();
            }
            return collection;
        }
        /// <summary>
        /// Sorting by Company and Name for any collection
        /// </summary>
        /// <param name="collection">Collection for sorting</param>
        /// <param name="order">Write descenting for sorting in desc order</param>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<Supplier> SortByName(IEnumerable<Supplier> collection, string order = "ascending")
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (order == "descending")
            {
                collection = collection.OrderByDescending(x => x.CompanyName).ThenByDescending(x => x.LastName).ToList();
            }
            else
            {
                collection = collection.OrderBy(x => x.CompanyName).ThenBy(x => x.LastName).ToList();
            }
            return collection;
        }
        /// <summary>
        /// Sorting by Price of products for suppliers
        /// </summary>
        /// <param name="collection">Collection for sorting</param>
        /// <param name="order">Write descenting for sorting in desc order</param>
        /// <exception cref="ArgumentNullException">Collection may be null</exception>

        public IEnumerable<Supplier> SortByPrice(IEnumerable<Supplier> collection, string order = "ascending")
        {
            if (collection == null || collection.Where(x => x.Products == null).Any())
                throw new ArgumentNullException(nameof(collection));
            IEnumerable<Supplier> suppliers;
            if (order == "descending")
            {
                suppliers = collection
                    .OrderByDescending(x => x.Products.Select(x => x.Price).Sum())
                    .ThenByDescending(x => x.CompanyName).ToList();
            }
            else
            {
                suppliers = collection
                    .OrderBy(x => x.Products.Select(x => x.Price).Sum())
                    .ThenByDescending(x => x.CompanyName).ToList();
            }
            return suppliers;
        }
    }
}
