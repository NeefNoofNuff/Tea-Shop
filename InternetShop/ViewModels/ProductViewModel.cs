namespace InternetShop.ViewModels
{
    public class ProductViewModel
    {
        // model for pages
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double UnitInStock { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string ImagePath { get; set; }

    }
}
