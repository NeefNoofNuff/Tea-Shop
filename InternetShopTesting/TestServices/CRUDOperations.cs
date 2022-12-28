using ShoppingSystemWeb.Data;
using ShoppingSystemWeb.Models;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace TestProject
{
    internal class CRUDOperations
    {
        private ShoppingSystemWebContext _context;

        public CRUDOperations(ShoppingSystemWebContext context)
        {
            _context = context;
        }

        public void AddProduct(int id, string title, DateTime expiredDate, string category, decimal price)
        {
            _context.Product.Add(new Product {Id = id, Title = title, ExpiredDate = expiredDate, Category = category, Price = price});
            _context.SaveChanges();
        }
        public void UpdateProduct(int id, string newTitle)
        {
            _context.Product.FirstOrDefault(i => i.Id == id).Title = newTitle;
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            _context.Product.Remove(_context.Product.Where(i => i.Id == id).FirstOrDefault());
            _context.SaveChanges();
        }

        public List<Product> GetAllProducts()
        {
            var query = from b in _context.Product orderby b.Title select b;

            return query.ToList();
        }
    }
}
