using System;
using Xunit;
using WebApplication2.Controllers;
using WebApplication2.Models;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace lab2.tests
{
    public class UnitTest1
    {
        [Fact]
        public void ProductsGetAll()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
            }.AsQueryable<Product>());

            AdminController controller = new AdminController(mock.Object);

            Product[] ViewModel = GetViewModel<IEnumerable<Product>>(controller.Index())?.ToArray();

            Assert.Equal(3, ViewModel.Length);
            Assert.Equal("P1", ViewModel[0].Name);
            Assert.Equal("P2", ViewModel[1].Name);
            Assert.Equal("P3", ViewModel[2].Name);
        }

        [Fact]
        public void ProductsFilter()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = new Category{Name="Cat1"} },
                new Product {ProductId = 2, Name = "P2", Category = new Category{Name="Cat2"} },
                new Product {ProductId = 3, Name = "P3", Category = new Category{Name="Cat1"} },
                new Product {ProductId = 4, Name = "P4", Category = new Category{Name="Cat2"} },
                new Product {ProductId = 5, Name = "P5", Category = new Category{Name="Cat3"} }
            }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object);

            Product[] ViewModel = GetViewModel<IEnumerable<Product>>(controller.List("Cat2")).ToArray();

            Assert.Equal(2, ViewModel.Length);
            Assert.True(ViewModel[0].Name == "P2" && ViewModel[0].Category.Name == "Cat2");
            Assert.True(ViewModel[1].Name == "P4" && ViewModel[1].Category.Name == "Cat2");
        }

        [Theory]
        [InlineData(1, "P1")]
        [InlineData(4, "P4")]
        public void ProductsGetByID(int id, string expectedName)
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = new Category{Name="Cat1"} },
                new Product {ProductId = 2, Name = "P2", Category = new Category{Name="Cat2"} },
                new Product {ProductId = 3, Name = "P3", Category = new Category{Name="Cat1"} },
                new Product {ProductId = 4, Name = "P4", Category = new Category{Name="Cat2"} },
                new Product {ProductId = 5, Name = "P5", Category = new Category{Name="Cat3"} }
            }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object);

            Product ViewModel = GetViewModel<Product>(controller.GetItemById(id));

            Assert.Equal(ViewModel.Name, expectedName);
        }

        [Theory]
        [InlineData("Cat1", 1)]
        [InlineData("Cat2", 2)]
        public void ProductsGetByCategory(string categoryName, int expectedId)
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = new Category{Name="Cat1"} },
                new Product {ProductId = 2, Name = "P2", Category = new Category{Name="Cat2"} },
                new Product {ProductId = 3, Name = "P3", Category = new Category{Name="Cat1"} },
                new Product {ProductId = 4, Name = "P4", Category = new Category{Name="Cat2"} },
                new Product {ProductId = 5, Name = "P5", Category = new Category{Name="Cat3"} }
            }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object);

            Product ViewModel = GetViewModel<List<Product>>(controller.GetItemByCategoryName(categoryName)).First();

            Assert.Equal(ViewModel.ProductId, expectedId);
        }

        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}