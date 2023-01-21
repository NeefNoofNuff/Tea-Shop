using InternetShop.Controllers;
using InternetShop.Logic.Services.Interfaces;
using InternetShop.Data.Models;
using Moq;
using InternetShop.Logic.Services;
using X.PagedList;

namespace InternetShop.Testing
{
    public class SupplierContollerTest
    {
        private Mock<IPaging> _pagingTools;
        private Mock<ISupplierService> _supplierMock;
        
        public SupplierController GetController()
        {
            return new SupplierController(_supplierMock.Object, _pagingTools.Object);
        }

        [Fact]
        public async Task Get_Supplier_By_Existing_IdAsync()
        {
            // arrange
            _supplierMock = new Mock<ISupplierService>();
            _pagingTools = new Mock<IPaging>();
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
            _supplierMock = new Mock<ISupplierService>();
            _pagingTools = new Mock<IPaging>();
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
            _supplierMock = new Mock<ISupplierService>();
            _pagingTools = new Mock<IPaging>();
            Supplier testSupplier = new Supplier
            {
                Id = 7,
                FirstName = "Herbie",
                LastName = "Haley",
                CompanyName = "Tea Side",
                PhoneNumber = "0964512756"
            };
            _supplierMock.Setup(x => x.Delete(7)).Returns(Task.FromResult(testSupplier));

            var controller = GetController();

            var result = await controller.Delete(testSupplier.Id);

            Assert.IsType<Microsoft.AspNetCore.Mvc.RedirectToActionResult>(await Task.FromResult(result));
        }

        [Fact]

        public async Task GetAllSuppliers()
        {
            _supplierMock = new Mock<ISupplierService>();
            _pagingTools = new Mock<IPaging>();
            var productMock = new Mock<IProductService>();
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

            _pagingTools.Setup(x => x.PageViewSupplierAsync(PagingTools.SortingByNameDesc, "", 1)).Returns(Task.FromResult(testlist.ToPagedList(1, PagingTools.ElementsPerPage)));

            var result = await controller.Index(PagingTools.SortingByNameDesc, "", "", 1);

            Assert.Equal("Microsoft.AspNetCore.Mvc.ViewResult", Task.FromResult(result).Result.ToString());
        }
    }
}