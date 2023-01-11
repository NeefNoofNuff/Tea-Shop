using InternetShop.Data.Models;

namespace InternetShop.Logic.Repository.Interfaces
{
    public interface IOrderRepository
    {
        public Task Create(Order order);
        public Task<Order> Get(int? id);
        public Task Update(Order order);
        public Task Delete(Order order);
        public Task<Order> Get(string firstName, string lastName, string phoneNumber);
    }
}
