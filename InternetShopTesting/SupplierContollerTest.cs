using InternetShop.Data;
using Moq;
using TaskAuthenticationAuthorization.Controllers;
using TaskAuthenticationAuthorization.Models;

namespace InternetShopTesting
{
    public class SupplierContollerTest
    {   
        private readonly ProductsController _productController;
        private Mock<ShoppingContext> _contextMock;

        public ProductsController GetController()
        {
            return new ProductsController(_contextMock.Object);
        }

        [Fact]
        public void Test1()
        {
            // arrange
            _contextMock = new Mock<ShoppingContext>();
            IEnumerable<Product> testCollection = new List<Product> { new Product() };
            _contextMock.Setup(x => x.GetAll()).Returns(Task.FromResult(testCollection));

            var controller = GetController();
            // act
            var result = await controller.GetAll();
            // assert

        }
    }
}