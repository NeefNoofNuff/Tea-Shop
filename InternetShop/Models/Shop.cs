namespace InternetShop.Models
{
    public class Shop
    {
        public string Address { get; set; } = string.Empty;

        public string Hours { get; set; } = string.Empty;

        public Shop(string address, string workingHours)
        {
            Address = address;
            Hours = workingHours;
        }
    }
}
