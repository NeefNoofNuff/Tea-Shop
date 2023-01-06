using InternetShop.Controllers;
using InternetShop.Data.Models;
using InternetShop.Logic.Repository.Interfaces;
using Moq;

namespace InternetShopTesting
{
    public class ProductControllerTests
    {
        private readonly SupplierController _productController;
        private Mock<ISupplierRepository> _supplierMock;
        private Mock<IShoppingRepository> _productMock;
        public ProductsController GetController()
        {
            return new ProductsController(_productMock.Object, _supplierMock.Object);
        }

        [Fact]
        public async Task Get_Product_By_Existing_IdAsync()
        {
            // arrange
            _supplierMock = new Mock<ISupplierRepository>();
            _productMock = new Mock<IShoppingRepository>();
            var testid = 817;
            Product testProduct = new Product
            {
                Id = 817,
                Name = "Shen Puer Tea «Lapis Dragon»",
                Price = 223,
                UnitInStock = 50.0,
                SupplierId = 1
            };

            _productMock.Setup(x => x.Get(testid)).Returns(Task.FromResult(testProduct));

            var controller = GetController();
            // act
            var result = await controller.Edit(testid);
            // assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Create_ProductAsync()
        {
            _supplierMock = new Mock<ISupplierRepository>();
            _productMock = new Mock<IShoppingRepository>();
            Product testProduct = new Product
            {
                Id = 817,
                Name = "Shen Puer Tea «Lapis Dragon»",
                Price = 223,
                UnitInStock = 50.0,
                SupplierId = 1
            };
            _productMock.Setup(x => x.Create(It.IsAny<Product>())).Returns(Task.CompletedTask);
            var controller = GetController();

            var result = await controller.Create(testProduct);

            //var deleting = controller.ProductExists(testProduct.Id);

            Assert.NotNull(result);
        }
        [Fact]
        public async Task Deleted_Product()
        {
            _supplierMock = new Mock<ISupplierRepository>();
            _productMock = new Mock<IShoppingRepository>();
            Product testProduct = new Product
            {
                Id = 817,
                Name = "Shen Puer Tea «Lapis Dragon»",
                Price = 223,
                UnitInStock = 50.0,
                SupplierId = 1
            };
            _productMock.Setup(x => x.Delete(It.IsAny<Product>())).Returns(Task.FromResult(testProduct));

            var controller = GetController();

            var result = await controller.Delete(testProduct.Id);

            Assert.Equal("Microsoft.AspNetCore.Mvc.NotFoundResult", Task.FromResult(result).Result.ToString());
        }

        [Fact]

        public Task GetAllProducts()
        {
            _productMock = new Mock<IShoppingRepository>();
            _supplierMock = new Mock<ISupplierRepository>();
            Product testProduct = new Product
            {
                Id = 817,
                Name = "Shen Puer Tea «Lapis Dragon»",
                Price = 223,
                UnitInStock = 50.0,
                SupplierId = 1
            };

            List<Product> testlist = new List<Product>();
            testlist.Add(testProduct);

            _productMock.Setup(x => x.GetAll())
                .Returns(Task.FromResult((IEnumerable<Product>)testlist));

            var controller = GetController();
            return Task.CompletedTask;
        }
    }
}
