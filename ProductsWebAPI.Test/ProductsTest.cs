using ProductsWebAPI.Business;
using ProductsWebAPI.Models;
using System;
using System.Linq;
using Xunit;

namespace ProductsWebAPI.Test
{
    public class ProductsTest : IDisposable
    {
        private readonly ProductsModel Product;

        public ProductsTest()
        {
            //Onetime setup
            Product = new ProductsModel()
            {
                ProductId = Guid.NewGuid().ToString(),
                ProductName = "Mouse",
                Category = "Computer Part",
                UnitPrice = 10
            };

            new ProductsBusiness().AddProducts(Product);
        }

        [Fact]
        public void GetProductsByIdTest()
        {
            //Act
            var productAdded = new ProductsBusiness().GetProductById(Product.ProductId);

            //Assert
            Assert.Equal(Product.ProductName, productAdded.ProductName);
            Assert.Equal(Product.Category, productAdded.Category);
            Assert.Equal(Product.UnitPrice, productAdded.UnitPrice);
        }

        [Fact]
        public void GetProductsTest()
        {
            //Act
            var products = new ProductsBusiness().GetProducts();

            //Assert
            Assert.True(products.Any());
        }

        [Theory]
        [InlineData("Monitor", "Eletronic", 1000.00)]
        public void UpdateProductTest(string productName, string category, decimal unitPrice)
        {
            //Arrange
            var product = new ProductsModel()
            {
                ProductId = Product.ProductId,
                ProductName = productName,
                Category = category,
                UnitPrice = unitPrice
            };

            //Act
            new ProductsBusiness().UpdateProduct(product);
            var productUpdated = new ProductsBusiness().GetProductById(Product.ProductId);

            //Assert
            Assert.NotEqual(Product.ProductName, productUpdated.ProductName);
            Assert.NotEqual(Product.Category, productUpdated.Category);
            Assert.NotEqual(Product.UnitPrice, productUpdated.UnitPrice);
        }

        [Theory]
        [InlineData("", "Eletronic", 1000.00)]
        [InlineData("Keyboard", "", 1000.00)]
        [InlineData("Keyboard", "Eletronic", 0)]
        public void AddProductsWithInvalidValuesException(string productName, string category, decimal unitPrice)
        {
            //Arrange
            var product = new ProductsModel()
            {
                ProductId = Product.ProductId,
                ProductName = productName,
                Category = category,
                UnitPrice = unitPrice
            };

            //Assert
            Assert.Throws<ArgumentNullException>(() => new ProductsBusiness().AddProducts(product));
        }

        [Fact]
        public void GetInexistentProductsException()
        {
            //Assert
            Assert.Throws<InvalidOperationException>(() => new ProductsBusiness().GetProductById(Guid.NewGuid().ToString()));
        }

        [Fact]
        public void DeleteInexistentProductException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new ProductsBusiness().DeleteProductById(Guid.NewGuid().ToString()));
        }

        [Fact]
        public void UpdateInexistentProduct()
        {
            //Arrange
            var product = new ProductsModel()
            {
                ProductId = Guid.NewGuid().ToString(),
                ProductName = "Tablet"
            };
            
            //Act
            var modifiedCount = new ProductsBusiness().UpdateProduct(product);
            
            //Assert
            Assert.True(modifiedCount <= 0);
        }

        public void Dispose()
        {
            new ProductsBusiness().DeleteProductById(Product.ProductId);
        }
    }
}