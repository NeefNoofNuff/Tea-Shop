using EntityFrameworkCoreMock.NSubstitute;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ShoppingSystemWeb.Controllers;
using ShoppingSystemWeb.Data;
using ShoppingSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    internal class ProductControllerTest
    {
        static DbContextOptions<ShoppingSystemWebContext> DummyOptions { get; } = new DbContextOptionsBuilder<ShoppingSystemWebContext>().Options;

        static DbContextMock<ShoppingSystemWebContext> dbContextMock = new DbContextMock<ShoppingSystemWebContext>(DummyOptions);

        DbSetMock<Product> productsDbSetMock = dbContextMock.CreateDbSetMock(x => x.Product, data);

        static ProductsController _controller = new ProductsController(dbContextMock.Object);

        static List<Product> data = new List<Product>
            {
                new Product {Id = 1, Title = "Title1", ExpiredDate = new DateTime(2012,9,15), Category = "Category2", Price = 10},
                new Product {Id = 2, Title = "Title3", ExpiredDate = new DateTime(2017,11,6), Category = "Category2", Price = 50},
                new Product {Id = 3, Title = "Title2", ExpiredDate = new DateTime(2021,4,11), Category = "Category1", Price = 100},
            };

        [Test]
        public async Task IndexMethodIsReturningViewResult()
        {
            Assert.IsInstanceOf<ViewResult>(await _controller.Index(1, string.Empty));
        }
        
        [Test]
        public async Task CreateMethodIsReturningViewResult()
        {
            Assert.IsInstanceOf<ViewResult>(_controller.Create());
        }

        [Test]
        public async Task CreateMethodIsCorrect()
        {
            var productToAdd = new Product()
            {
                Id = 7,
                Title = "Title7",
                ExpiredDate = new DateTime(2012, 10, 15),
                Category = "Category1",
                Price = 100M
            };

            var created = await _controller.Create(productToAdd);

            var productToFind = productsDbSetMock.Object.FirstOrDefault(x => x.Id == productToAdd.Id);

            Assert.IsNotNull(productToFind);
        }

    }
}
