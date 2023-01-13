using InternetShop.Data.Models;
using InternetShop.Logic.Filtering.Interfaces;
using InternetShop.Logic.Services;

namespace InternetShop.Logic.Filtering
{
    public class ProductCollectionOrdering : IEnumerableOrdering<Product>
    {
        public IEnumerable<Product> Sort(IEnumerable<Product> collection, string sortOrder)
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

        /// <summary>
        /// Sorting by Id for products collection
        /// </summary>
        /// <param name="collection">Collection for sorting</param>
        /// <param name="order">Write descenting for sorting in desc order</param>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<Product> SortById(IEnumerable<Product> collection, string order = "ascending")
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
        /// Sorting by Name for products collection
        /// </summary>
        /// <param name="collection">Collection for sorting</param>
        /// <param name="order">Write descenting for sorting in desc order</param>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<Product> SortByName(IEnumerable<Product> collection, string order = "ascending")
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            IEnumerable<Product> result;
            if (order == "descending")
            {
                result = collection.OrderByDescending(x => x.Name).ToList();
            }
            else
            {
                result = collection.OrderBy(x => x.Name).ToList();
            }
            return result;
        }
        /// <summary>
        /// Sorting by Price for products collection
        /// </summary>
        /// <param name="collection">Collection for sorting</param>
        /// <param name="order">Write descenting for sorting in desc order</param>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<Product> SortByPrice(IEnumerable<Product> collection, string order = "ascending")
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            IEnumerable<Product> result;
            if (order == "descending")
            {
                result = collection.OrderByDescending(x => x.Price).ToList();
            }
            else
            {
                result = collection.OrderBy(x => x.Price).ToList();
            }
            return result;
        }


    }
}
