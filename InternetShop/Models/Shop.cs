namespace InternetShop.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public string Address { get; set; } = string.Empty;

        public string Hours { get; set; } = string.Empty;

        public Shop(int id, string address, string workingHours)
        {
            Id = id;
            Address = address;
            Hours = workingHours;
        }
    }
}
