using Neo4j.Driver;
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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        public bool Confirmed { get; set; }

        public ICollection<OrderDetail> Details { get; set; }

        [NotMapped]
        public List<int> ProductsId { get; set; }

        public Order()
        {
            Details = new List<OrderDetail>();
        }
        
    }
}
