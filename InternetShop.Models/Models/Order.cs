﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CouchDB.Driver.Types;

namespace InternetShop.Models
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
        public int ProductId { get; set; } 
        public Product Product { get; set; }
    }
}