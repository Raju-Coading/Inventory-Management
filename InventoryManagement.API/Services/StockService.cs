using InventoryManagement.API.Data;
using InventoryManagement.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.API.Services
{
    public class StockService : IStockService
    {
        
        private readonly IProductRepository _repository;
        private readonly ILogger<StockService> _logger;

        public StockService(ApplicationDbContext context, ILogger<StockService> logger)
        {
           
            _logger = logger;
        }

        public async Task<int?> GetStockAsync(int productId)
        {
            _logger.LogInformation("Getting stock for product ID {ProductId}.", productId);
            var product = await _repository.GetByIdAsync(productId);

            if (product == null)
                return null;

            return product.StockQuantity;
        }

        public async Task<bool> AddStockAsync(int productId, int quantity)
        {
            if (quantity <= 0)
                return false;

            var product = await _repository.GetByIdAsync(productId);

            if (product == null)
                return false;

            product.StockQuantity += quantity;

            await _repository.UpdateAsync(product);

            return true;
        }

        public async Task<bool> ReduceStockAsync(int productId, int quantity)
        {
            if (quantity <= 0)
                return false;

            var product = await _repository.GetByIdAsync(productId);

            if (product == null)
                return false;

          
            if (product.StockQuantity < quantity)
                return false;

            product.StockQuantity -= quantity;

            await _repository.UpdateAsync(product);

            return true;
        }
    }
}