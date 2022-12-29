using InternetShop.Controllers;
using InternetShop.Data.Repository;
using InternetShop.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //new Product
            //{
            //    Id = 560,
            //    Name = "Garnet Black Tea",
            //    Price = 86.85,
            //    UnitInStock = 150.0,
            //    SupplierId = 1
            //}
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
            _productMock.Setup(x => x.Delete(It.IsAny<Product>())).Returns(Task.CompletedTask);
            var controller = GetController();

            var result = await controller.Delete(testProduct.Id);

            var deleting = controller.ProductExists(testProduct.Id);

            Assert.False(deleting);
        }
        [Fact]
        public async Task Deleted_Supplier()
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

        public async Task GetAllSuppliers()
        {
            _productMock = new Mock<IShoppingRepository>();
            _supplierMock = new Mock<ISupplierRepository>();
            Supplier testSupplier = new Supplier
            {
                Id = 7,
                FirstName = "Herbie",
                LastName = "Haley",
                CompanyName = "Tea Side",
                PhoneNumber = "0964512756"
            };

            List<Supplier> testlist = new List<Supplier>();
            testlist.Add(testSupplier);

            _productMock.Setup(x => x.GetAllSuppliers())
                .Returns((IEnumerable<Supplier>)testlist);

            var controller = GetController();

            var result = await controller.Index();

            Assert.Equal("Microsoft.AspNetCore.Mvc.ViewResult", Task.FromResult(result).Result.ToString());
        }
    }
}
