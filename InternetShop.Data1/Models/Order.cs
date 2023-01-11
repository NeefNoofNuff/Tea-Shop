﻿using Neo4j.Driver;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShop.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Price { get; set; }
        public string PhoneNumber { get; set; }
        public float UnitsCount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public ICollection<Product> Products { get; set; }
        [NotMapped]
        public List<int> ProductsId { get; set; }
        [NotMapped]
        public Dictionary<Product, int> UnitPerProduct { get; set; }
        
    }
}
