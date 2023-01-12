using InternetShop.Data.Models;
using InternetShop.Logic.Repository.Interfaces;
using InternetShop.Logic.Services.Interfaces;

namespace InternetShop.Logic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingRepository _shoppingRepository;
        private readonly InvoiceFactory _invoiceFactory;

        public OrderService(IOrderRepository orderRepository, 
            IShoppingRepository shoppingRepository, 
            InvoiceFactory invoiceFactory)
        {
            _orderRepository = orderRepository;
            _shoppingRepository = shoppingRepository;
            _invoiceFactory = invoiceFactory;
        }

        public async Task Delete(int? id)
        {
            try
            {
                var order = await Get(id);
                await _orderRepository.Delete(order);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Order> Get(int? id)
        {
            try
            {
                var order = await _orderRepository.Get(id);
                if(order == null)
                    throw new NullReferenceException("Order was not found!");
                return await _orderRepository.Get(id);

            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task AddProductsInDetails(int? id, ICollection<int> productIds)
        {
            try
            {
                var order = await Get(id);
                foreach (var prodId in productIds)
                {
                    var product = await _shoppingRepository.Get(prodId);
                    if (product == null)
                    {
                        throw new NullReferenceException("Product was not found!");
                    }
                    order.Details.Add(new OrderDetail(order, order.Id, product, prodId, 1));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CalculatePrice(int? id)
        {
            try
            {
                var order = await Get(id);
                if (order == null)
                    throw new NullReferenceException("Order was not found!");
                order.Price = order.Details
                    .Select(x => x.Product.Price * x.Units)
                    .Sum()
                    .ToString();
                await _orderRepository.Update(order);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Order> CreateDefaultOrder(Order order)
        {
            if (order == null)
                throw new NullReferenceException("order is null!");

            order.Details = new List<OrderDetail>(order.ProductsId.Count);
            order.Price = "0";
            order.OrderDate = DateTime.Now;

            await _orderRepository.Create(order);
            order = await _orderRepository.Get(order.FirstName, order.LastName, order.PhoneNumber);

            if (order == null)
            {
                throw new NullReferenceException("Order was added with en error");
            }

            await AddProductsInDetails(order.Id, order.ProductsId);

            foreach (var detail in order.Details)
            {
                detail.OrderId = order.Id;
                detail.Order = order;
            }

            await _orderRepository.Update(order);
            return order;
        }

        public async Task CreateInvoice(int? id)
        {
            try
            {
                var order = await _orderRepository.Get(id);
                order.Confirmed = true;
                await _orderRepository.Update(order);
                await _invoiceFactory.Create(order);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<int?> UpdateDetails(int? id, ICollection<OrderDetail> details)
        {
            var order = await _orderRepository.Get(id);
            
            foreach (var detail in details)
            {
                if(detail.Units == 0)
                {
                    details.Remove(detail);
                }
            }
            order.Details = details;
            foreach (var detail in order.Details)
            {
                var product = await _shoppingRepository.Get(detail.ProductId);
                if (product == null)
                    throw new NullReferenceException("Product was not found!");
                detail.Product = product;
            }
            if (_shoppingRepository.ReduceUnitStockAsync(order.Details))
            {
                await _orderRepository.Update(order);
            }
            await CalculatePrice(id);
            return id;
        }
    }
}
