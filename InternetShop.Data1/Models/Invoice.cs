using CouchDB.Driver.Types;
using System.ComponentModel.DataAnnotations;

namespace InternetShop.Data.Models
{
    public class Invoice : CouchDocument
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Price { get; set; }
        public string PhoneNumber { get; set; }
        public float UnitsCount { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public List<int> ProductIds { get; set; }
        public List<string> ProductNames { get;set; }
        public List<string> UnitsPerProduct { get; set; } 

        public Invoice(Order order)
        {
            Id = new Guid();
            FirstName = order.FirstName;
            LastName = order.LastName;
            Price = order.Price;
            PhoneNumber = order.PhoneNumber;
            OrderDate = order.OrderDate;
            ProductIds = order.ProductsId;

            ProductNames = order.Details
                .Select(x => x.Product.Name).ToList();

            UnitsPerProduct = order.Details
                .Select(d => d.Product.Name + ": " + d.Units.ToString()).ToList();
        }
    }
}
