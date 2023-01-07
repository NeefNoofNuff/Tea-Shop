using InternetShop.Data.Context;
using InternetShop.Data.Models;

namespace InternetShopTesting
{
    internal class ShoppingQuery
    {
        private ShoppingContext _context;

        public ShoppingQuery(ShoppingContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> ExecuteProduct()
        {
            return _context.Products.Select(product => product);
        }

        public IEnumerable<Supplier> ExecuteSuppliers()
        {
            return _context.Suppliers.Select(supp => supp);
        }
    }
}
