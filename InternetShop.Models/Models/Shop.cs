
using System.ComponentModel.DataAnnotations;

namespace InternetShop.Models
{
    public class Shop
    {
        //[Required]
        public int Id { get; set; }
        public string Address { get; set; }

        public string Hours { get; set; }

        public Shop()
        {
            Id = -1;
            Address = string.Empty;
            Hours = string.Empty;
        }

        public Shop(int id, string address, string workingHours)
        {
            Id = id;
            Address = address;
            Hours = workingHours;
        }
    }
}
