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
        public int ProductId { get; set; }
        public string ProductName { get;set; }

        public Invoice(Order order)
        {
            Id = new Guid();
            FirstName = order.FirstName;
            LastName = order.LastName;
            Price = order.Price;
            PhoneNumber = order.PhoneNumber;
            UnitsCount = order.UnitsCount;
            OrderDate = DateTime.Now;
            ProductId = order.ProductId;
            ProductName = order.Product.Name;
        }
    }
}
