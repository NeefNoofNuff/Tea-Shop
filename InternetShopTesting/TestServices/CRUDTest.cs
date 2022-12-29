using InternetShop.Data;
using TaskAuthenticationAuthorization.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Reflection.Metadata;
using static System.Reflection.Metadata.BlobBuilder;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace TestProject
{
    public class Tests
    {
        private CRUDOperations operations;
        private Mock<DbSet<Product>> mockSet;
        private Mock<ShoppingContext> mockContext;

        [SetUp]
        public void Setup()
        {
            var data = new List<Product>
            {
                new Product {Id = 1, Title = "Title1", ExpiredDate = new DateTime(2012,9,15), Category = "Category2", Price = 10},
                new Product {Id = 2, Title = "Title3", ExpiredDate = new DateTime(2017,11,6), Category = "Category2", Price = 50},
                new Product {Id = 3, Title = "Title2", ExpiredDate = new DateTime(2021,4,11), Category = "Category1", Price = 100},
            }.AsQueryable();

            mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            mockContext = new Mock<ShoppingContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);
            operations = new CRUDOperations(mockContext.Object);
        }

        [Test]
        public void TestGetAllProducts_ordered_by_title()
        {
            var products = operations.GetAllProducts();
            Assert.AreEqual(3, products.Count);
            Assert.AreEqual("Title1", products[0].Title);
            Assert.AreEqual("Title2", products[1].Title);
            Assert.AreEqual("Title3", products[2].Title);
        }
        [Test]
        public void TestAddProduct()
        {
            operations.AddProduct(4, "Title4", new DateTime(2012, 10, 15), "Category2", 100);
            mockSet.Verify(m => m.Add(It.IsAny<Product>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
        [Test]
        public void TestUpdateProduct()
        {
            operations.UpdateProduct(1, "Title10");
            var products = operations.GetAllProducts();
            Assert.AreEqual("Title10", products.Find(i => i.Id == 1).Title);
            Assert.AreNotEqual("Title1", products.Find(i => i.Id == 1).Title);
        }
        [Test]
        public void TestDeleteProduct()
        {
            operations.DeleteProduct(1);
            var products = operations.GetAllProducts();
            mockSet.Verify(m => m.Remove(It.IsAny<Product>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}