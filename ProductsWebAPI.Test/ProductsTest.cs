using Xunit;
using Moq;
using System;
using ProductsWebAPI.Models;
using ProductsWebAPI.Business;

namespace ProductsWebAPI.Test
{
    public class ProductsTest
    {
        [Theory]
        [InlineData("KeyBoard", "Computer Parts", 10.00)]
        [InlineData("Mouse", "Computer Parts", 30.00)]
        public void GetProductById(string productName, string category, decimal unitPrice)
        {
        }
    }
}