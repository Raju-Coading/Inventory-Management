using InventoryManagement.API.Models;
using InventoryManagement.API.Repositories;

namespace InventoryManagement.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            return await _repository.CreateAsync(product);
        }

        public async Task<bool> UpdateAsync(int id, Product product)
        {
            if (id != product.Id)
                return false;

            var existingProduct = await _repository.GetByIdAsync(id);

            if (existingProduct == null)
                return false;

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.StockQuantity = product.StockQuantity;

            await _repository.UpdateAsync(existingProduct);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return false;

            await _repository.DeleteAsync(product);

            return true;
        }
    }
}