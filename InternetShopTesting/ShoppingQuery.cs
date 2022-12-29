using InternetShop.Data;
using InternetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
