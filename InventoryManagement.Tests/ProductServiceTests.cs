
using InventoryManagement.API.Models;
using InventoryManagement.API.Repositories;
using InventoryManagement.API.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace InventoryManagement.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task GetAllAsyncProducts()
        {
            var repository = new Mock<IProductRepository>();
            var logger = new Mock<ILogger<ProductService>>();

            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Laptop",
                    Price = 50000,
                    StockQuantity = 10
                }
            };

            repository.Setup(r => r.GetAllAsync())
                      .ReturnsAsync(products);

            var service = new ProductService(
                repository.Object,
                logger.Object);

            var result = await service.GetAllAsync();

            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdAsyncProduct()
        {
            
            var repository = new Mock<IProductRepository>();
            var logger = new Mock<ILogger<ProductService>>();

            var product = new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 50000,
                StockQuantity = 10
            };

            repository.Setup(r => r.GetByIdAsync(1))
                      .ReturnsAsync(product);

            var service = new ProductService(
                repository.Object,
                logger.Object);

           
            var result = await service.GetByIdAsync(1);

            
            Assert.NotNull(result);
            Assert.Equal("Laptop", result.Name);
        }

        [Fact]
        public async Task DeleteAsynctProduct()
        {
           
            var repository = new Mock<IProductRepository>();
            var logger = new Mock<ILogger<ProductService>>();

            repository.Setup(r => r.GetByIdAsync(99))
                      .ReturnsAsync((Product?)null);

            var service = new ProductService(
                repository.Object,
                logger.Object);

            
            var result = await service.DeleteAsync(99);

            
            Assert.False(result);
        }

        [Fact]
        public async Task 
            
            
            
            
            
            
            CreateAsyncProduct()
        {
           
            var repository = new Mock<IProductRepository>();
            var logger = new Mock<ILogger<ProductService>>();

            var product = new Product
            {
                Id = 1,
                Name = "Mouse",
                Price = 500,
                StockQuantity = 10
            };

            repository.Setup(r => r.CreateAsync(product))
                      .ReturnsAsync(product);

            var service = new ProductService(
                repository.Object,
                logger.Object);

            var result = await service.CreateAsync(product);

      
            Assert.NotNull(result);
            Assert.Equal("Mouse", result.Name);
        }

        [Fact]
        public async Task UpdateAsyncProduct()
        {
           
            var repository = new Mock<IProductRepository>();
            var logger = new Mock<ILogger<ProductService>>();

            var product = new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 50000,
                StockQuantity = 10
            };

            var service = new ProductService(
                repository.Object,
                logger.Object);

           
            var result = await service.UpdateAsync(2, product);

            Assert.False(result);
        }
    }
}
