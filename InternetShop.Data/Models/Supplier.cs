using InternetShop.Models;

namespace InternetShop.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
