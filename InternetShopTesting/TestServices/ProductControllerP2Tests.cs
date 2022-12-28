using NUnit.Framework;
using EntityFrameworkCoreMock.NSubstitute;
using Moq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;

using ShoppingSystemWeb.Controllers;
using ShoppingSystemWeb.Data;
using ShoppingSystemWeb.Models;
using System.Reflection;

namespace TestProject
{
    
    public class ProductControllerP2Tests
    {
        private List<Product> _data = new List<Product>
        {
            new Product {Id = 1, Title = "Title1", ExpiredDate = new DateTime(2012,9,15), Category = "Category2", Price = 10},
            new Product {Id = 2, Title = "Title3", ExpiredDate = new DateTime(2017,11,6), Category = "Category2", Price = 50},
            new Product {Id = 3, Title = "Title2", ExpiredDate = new DateTime(2021,4,11), Category = "Category1", Price = 100}
        };

        private ProductsController _controller;
        private DbContextMock<ShoppingSystemWebContext> _dbContextMock;
        private DbSetMock<Product> _dbSetMock;
        private DbContextOptions<ShoppingSystemWebContext> _dummyOptions { get; } = new DbContextOptionsBuilder<ShoppingSystemWebContext>().Options;

        [SetUp]
        public void TestSetup()
        {
            _dbContextMock = new DbContextMock<ShoppingSystemWebContext>(_dummyOptions);
            _dbSetMock = _dbContextMock.CreateDbSetMock(x => x.Product, _data);
            _controller = new ProductsController(_dbContextMock.Object);
        }
        #region Details
        [Test]
        public async Task DetailsReturnsNotFound()
        {
            var result = await _controller.Details(null);
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);

            result = await _controller.Details(-1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task DetailsReturnsView()
        {
            var result = await _controller.Details(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task DetailsViewModelIsCorrect()
        {
            var id = 1;
            var result = await _controller.Details(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.IsInstanceOf<Product>(viewResult.Model);

            var modelJson = JsonSerializer.Serialize(viewResult.Model as Product);
            var expectedJson = JsonSerializer.Serialize(_data.FirstOrDefault(p => p.Id == id));
            Assert.AreEqual(modelJson, expectedJson);
        }
        #endregion
        #region [GET]Edit
        [Test]
        public async Task EditReturnsNotFound()
        {
            var result = await _controller.Edit(null);
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);

            result = await _controller.Edit(-1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task EditReturnsView()
        {
            var result = await _controller.Edit(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task EditViewModelIsCorrect()
        {
            var id = 1;
            var result = await _controller.Edit(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.IsInstanceOf<Product>(viewResult.Model);

            var modelJson = JsonSerializer.Serialize(viewResult.Model as Product);
            var expectedJson = JsonSerializer.Serialize(_data.FirstOrDefault(p => p.Id == id));
            Assert.That(expectedJson, Is.EqualTo(modelJson));
        }
        #endregion
        #region [POST]Edit
        [Test]
        public async Task EditPostReturnsNotFound()
        {
            var result = await _controller.Edit(-1, new Product{});
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task EditPostReturnsIndex()
        {
           var product = new Product{Id=1, Title="TestTitle", ExpiredDate=DateTime.Now, Category="TestCategory", Price=1};
            var result = await _controller.Edit(1, product);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);

            var actionResult = result as RedirectToActionResult ;
            Assert.AreEqual(actionResult.ActionName, nameof(_controller.Index));
        }

        #endregion
        #region [GET]Delete
        public async Task DeleteReturnsNotFound()
        {
            var result = await _controller.Delete(null);
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);

            result = await _controller.Delete(-1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task DeleteReturnsView()
        {
            var result = await _controller.Delete(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }
        #endregion
        #region DeleteConfirmed
        [Test]
        public async Task DeleteConfirmedReturnsIndex()
        {
            var result = await _controller.DeleteConfirmed(1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);

            var actionResult = result as RedirectToActionResult ;
            Assert.AreEqual(actionResult.ActionName, nameof(_controller.Index));
        }

        [Test]
        public async Task DeleteConfirmedRemovesProduct()
        {
            var result = await _controller.DeleteConfirmed(2);
            var product = _dbSetMock.Object.FirstOrDefault(p => p.Id == 2);

            Assert.Null(product);
        }
        #endregion
    }
}