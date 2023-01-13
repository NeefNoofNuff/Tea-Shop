using InternetShop.Data.Models;
using InternetShop.Logic.Filtering.Interfaces;

namespace InternetShop.Logic.Filtering
{
    public class SupplierCollectionFilter : IEnumerableFilter<Supplier>
    {
        public IEnumerable<Supplier> Filtering(IEnumerable<Supplier> suppliers, IEnumerableFilter<Supplier>.FilterDelegate filter)
        {
            var collection = new List<Supplier>();
            foreach (var supplier in suppliers)
            {
                if (filter(supplier))
                {
                    collection.Add(supplier);
                }
            }
            return collection;
        }
    }
}
