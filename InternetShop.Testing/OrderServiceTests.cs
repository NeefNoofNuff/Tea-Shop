using InternetShop.Controllers;
using InternetShop.Logic.Services.Interfaces;
using InternetShop.Logic.Services;
using Moq;
using InternetShop.Logic.Repository.Interfaces;
using InternetShop.Data.Models;
using InternetShop.Logic.Repository;
using Microsoft.Build.Framework;

namespace InternetShop.Testing
{
    public class OrderServiceTests
	{
        private Mock<IOrderRepository> _orderRepoMock;
        private Mock<IShoppingRepository> _productMock;
        private Mock<IInvoiceRepository> _invoiceMock;
        private InvoiceFactory _invoiceFactoryMock;

        public IOrderService GetService()
        {
            return new OrderService(_orderRepoMock.Object, _productMock.Object, _invoiceFactoryMock);
        }
        public Order GetOrder()
        {
            return new Order
            {
                ProductsId = new List<int> { 1, 2 },
                FirstName = "TestName",
                LastName = "TestLastName",
                PhoneNumber = "0999999999"
            };
        }

        public List<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product
                {
                    Id = 1,
                    Price = 20
                },
                new Product
                {
                    Id = 2,
                    Price = 30
                }
            };
        }

        public List<OrderDetail> GetDetails(Order order, ICollection<Product> products)
        {
            return new List<OrderDetail>()
            {
                new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = 1,
                    Units = 1,
                    Product = products.First(),
                    Order = order
                },
                new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = 2,
                    Units = 2,
                    Product = products.Last(),
                    Order = order
                }
            };
        }

        [Fact]
        public async Task Create_Def_OrderAsync()
        {
            // arrange
            _orderRepoMock = new Mock<IOrderRepository>();
            _productMock = new Mock<IShoppingRepository>();
            _invoiceMock = new Mock<IInvoiceRepository>();
            _invoiceFactoryMock = new InvoiceFactory(_invoiceMock.Object);

            var order = GetOrder();
            var products = GetProducts();
            // act
            var service = GetService();

            _orderRepoMock.Setup(x => x.Create(order)).Returns(Task.FromResult(order));
            _orderRepoMock.Setup(x => x.Get(order.FirstName, order.LastName, order.PhoneNumber)).Returns(Task.FromResult(order));

            _orderRepoMock.Setup(x => x.Get(order.Id)).Returns(Task.FromResult(order));

            _orderRepoMock.Setup(x => x.Update(order)).Returns(Task.FromResult(order));

            _productMock.Setup(x => x.Get(1)).Returns(Task.FromResult(products.First()));
            _productMock.Setup(x => x.Get(2)).Returns(Task.FromResult(products.Last()));

            var result = await service.CreateDefaultOrder(order);

            // assert
            Assert.NotNull(result);
            Assert.Equal("0", result.Price);
            Assert.Equal(2, result.Details.Count);
        }
        [Fact]
        public async Task Add_Details_To_Order_Test()
        {
            // arrange
            _orderRepoMock = new Mock<IOrderRepository>();
            _productMock = new Mock<IShoppingRepository>();
            _invoiceMock = new Mock<IInvoiceRepository>();
            _invoiceFactoryMock = new InvoiceFactory(_invoiceMock.Object);
            var order = GetOrder();
            var products = GetProducts();
            var details = GetDetails(order, products);
            
            _orderRepoMock.Setup(x => x.Get(order.Id)).Returns(Task.FromResult(order));

            _orderRepoMock.Setup(x => x.Update(order)).Returns(Task.FromResult(order));

            _productMock.Setup(x => x.Get(1)).Returns(Task.FromResult(products.First()));
            _productMock.Setup(x => x.Get(2)).Returns(Task.FromResult(products.Last()));

            _productMock.Setup(x => x.ReduceUnitStockAsync(details)).Returns(true);
            // act
            var service = GetService();
            var result = await service.UpdateDetails(order.Id, details);
            // assert
            Assert.NotNull(order.Details);
            Assert.Equal(result.ToString(), order.Id.ToString());
            Assert.True(order.Details.Any());
            Assert.True(order.Details.Where(details => details.ProductId == 1).Any());
        }

        [Fact]
        public async Task Calculate_Price_Test()
        {
            // arrange
            _orderRepoMock = new Mock<IOrderRepository>();
            _productMock = new Mock<IShoppingRepository>();
            _invoiceMock = new Mock<IInvoiceRepository>();
            _invoiceFactoryMock = new InvoiceFactory(_invoiceMock.Object);

            var order = GetOrder();

            var products = GetProducts();
            var details = GetDetails(order, products);

            order.Details = details;

            _orderRepoMock.Setup(x => x.Get(order.Id)).Returns(Task.FromResult(order));

            _orderRepoMock.Setup(x => x.Update(order)).Returns(Task.FromResult(order));

            _productMock.Setup(x => x.Get(1)).Returns(Task.FromResult(products.First()));
            _productMock.Setup(x => x.Get(2)).Returns(Task.FromResult(products.Last()));
;
            // act
            var service = GetService();

            await service.CalculatePrice(order.Id);
            var newOrder = await service.Get(order.Id);
            // assert
            Assert.Equal("80", newOrder.Price);
        }
    }
}
