using InternetShop.Controllers;
using InternetShop.Data.Models;
using InternetShop.Logic.Repository.Interfaces;
using InternetShop.Logic.Services;
using InternetShop.Logic.Services.Interfaces;
using Moq;

namespace InternetShop.Testing
{
    public class ProductControllerTests
    {
        private readonly PagingTools _pagingTools;
        private Mock<ISupplierService> _supplierMock;
        private Mock<IProductService> _productMock;

        public ProductsController GetController()
        {
            return new ProductsController(_productMock.Object, _supplierMock.Object, _pagingTools);
        }

        [Fact]
        public async Task Get_Product_By_Existing_IdAsync()
        {
            // arrange
            _supplierMock = new Mock<ISupplierService>();
            _productMock = new Mock<IProductService>();
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
            _supplierMock = new Mock<ISupplierService>();
            _productMock = new Mock<IProductService>();
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

            Assert.NotNull(result);
        }
        [Fact]
        public async Task Delete_Product()
        {
            _supplierMock = new Mock<ISupplierService>();
            _productMock = new Mock<IProductService>();
            Product testProduct = new Product
            {
                Id = 100,
                Name = "Shen Puer Tea «Lapis Dragon» TEST",
                Price = 223,
                UnitInStock = 50.0,
                SupplierId = 1
            };
            _productMock.Setup(x => x.Delete(100)).Returns(Task.FromResult(testProduct));
            var controller = GetController();
            Assert.NotNull(controller);
            var result = await controller.Delete(testProduct.Id);
            Assert.IsType<Microsoft.AspNetCore.Mvc.ViewResult>(await Task.FromResult(result));
        }

        [Fact]

        public Task GetAllProducts()
        {
            _supplierMock = new Mock<ISupplierService>();
            _productMock = new Mock<IProductService>();
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
