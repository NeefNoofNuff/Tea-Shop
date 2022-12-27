using InternetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAuthenticationAuthorization.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double UnitInStock { get; set; }
        public Supplier Supplier { get; set; }
        public int SupplierId { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
