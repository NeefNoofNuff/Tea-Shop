using InternetShop.Data.Models;
using InternetShop.Logic.Filtering.Interfaces;

namespace InternetShop.Logic.Filtering
{
    public class ProductCollectionFilter : IEnumerableFilter<Product>
    {
        public IEnumerable<Product> Filtering(IEnumerable<Product> products,
            IEnumerableFilter<Product>.FilterDelegate filter)
        {
            var collection = new List<Product>();
            if (products == null)
                return collection;
            foreach (var product in products)
            {
                if (filter(product))
                {
                    collection.Add(product);
                }
            }
            return collection;
        }
    }
}
