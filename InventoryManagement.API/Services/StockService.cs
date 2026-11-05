using InventoryManagement.API.Repositories;

namespace InventoryManagement.API.Services
{
    public class StockService : IStockService
    {
        private readonly IProductRepository _repository;

        public StockService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<int?> GetStockAsync(int productId)
        {
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

            // Prevent negative stock
            if (product.StockQuantity < quantity)
                return false;

            product.StockQuantity -= quantity;

            await _repository.UpdateAsync(product);

            return true;
        }
    }
}