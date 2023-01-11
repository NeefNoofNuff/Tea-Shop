using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShop.Data.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double UnitInStock { get; set; }
        [Required]
        public int SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }
        
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
