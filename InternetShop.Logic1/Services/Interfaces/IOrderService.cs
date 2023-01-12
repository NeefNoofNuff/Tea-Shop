using InternetShop.Data.Models;

namespace InternetShop.Logic.Services.Interfaces
{
    public interface IOrderService
    {
        public Task CreateInvoice(int? id);
        public Task<Order> CreateDefaultOrder(Order order);
        public Task AddProductsInDetails(int? id, ICollection<int> productIds);
        public Task<int?> UpdateDetails(int? id, ICollection<OrderDetail> details);
        public Task CalculatePrice(int? id);
        public Task<Order> Get(int? id);
        public Task Delete(int? id);
    }
}
