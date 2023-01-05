using InternetShop.Controllers;
using InternetShop.Logic.Repository.Interfaces;
using InternetShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace InternetShopTesting
{
    public class SupplierContollerTest
    {   
        private readonly SupplierController _productController;
        private Mock<ISupplierRepository> _supplierMock;
        public SupplierController GetController()
        {
            return new SupplierController(_supplierMock.Object);
        }

        [Fact]
        public async Task Get_Supplier_By_Existing_IdAsync()
        {
            // arrange
            _supplierMock = new Mock<ISupplierRepository>();
            var testid = 5;
            Supplier testSupplier = new Supplier
            {
                Id = 7,
                FirstName = "Herbie",
                LastName = "Haley",
                CompanyName = "Tea Side",
                PhoneNumber = "0964512756"
            };
            _supplierMock.Setup(x => x.Get(testid)).Returns(Task.FromResult(testSupplier));

            var controller = GetController();
            // act
            var result = await controller.Edit(testid);
            // assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Create_SupplierAsync()
        {
            _supplierMock = new Mock<ISupplierRepository>();
            Supplier testSupplier = new Supplier
            {
                Id = 7,
                FirstName = "Herbie",
                LastName = "Haley",
                CompanyName = "Tea Side",
                PhoneNumber = "0964512756"
            };
            _supplierMock.Setup(x => x.Create(It.IsAny<Supplier>())).Returns(Task.CompletedTask);
            var controller = GetController();

            var result = await controller.Create(testSupplier);

            Assert.NotNull(result);
        }
        [Fact]
        public async Task Deleted_Supplier()
        {
            _supplierMock = new Mock<ISupplierRepository>();
            Supplier testSupplier = new Supplier
            {
                Id = 7,
                FirstName = "Herbie",
                LastName = "Haley",
                CompanyName = "Tea Side",
                PhoneNumber = "0964512756"
            };
            _supplierMock.Setup(x => x.Delete(It.IsAny<Supplier>())).Returns(Task.FromResult(testSupplier));

            var controller = GetController();

            var result = await controller.Delete(testSupplier.Id);

            Assert.Equal("Microsoft.AspNetCore.Mvc.NotFoundResult", Task.FromResult(result).Result.ToString());
        }

        [Fact]

        public async Task GetAllSuppliers()
        {
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

            _supplierMock.Setup(x => x.GetAll())
                .Returns(Task.FromResult((IEnumerable<Supplier>)testlist));
            
            var controller = GetController();

            var result = await controller.Index();

            Assert.Equal("Microsoft.AspNetCore.Mvc.ViewResult", Task.FromResult(result).Result.ToString());
        }
    }
}