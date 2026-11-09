using InventoryManagement.API.Models;
using InventoryManagement.API.Repositories;

namespace InventoryManagement.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductService> _logger;

        public ProductService( IProductRepository productRepository,ILogger<ProductService> logger)
        {
            _repository = productRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all products.");

            var products = await _repository.GetAllAsync();

            _logger.LogInformation(
                "Successfully fetched {ProductCount} products.",
                products.Count());

            return products;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            _logger.LogInformation(
                "Fetching product with ID {ProductId}.",
                id);

            var product = await _repository.GetByIdAsync(id);

            if (product == null)
            {
                _logger.LogWarning(
                    "Product with ID {ProductId} was not found.",
                    id);

                return null;
            }

            _logger.LogInformation(
                "Product with ID {ProductId} retrieved successfully.",
                id);

            return product;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _logger.LogInformation(
                "Creating product: {ProductName}.",
                product.Name);

            var createdProduct =
                await _repository.CreateAsync(product);

            _logger.LogInformation(
                "Product created successfully with ID {ProductId}.",
                createdProduct.Id);

            return createdProduct;
        }

        public async Task<bool> UpdateAsync(int id, Product product)
        {
            _logger.LogInformation("Updating product with ID {ProductId}.",id);

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

            _logger.LogInformation("Deleting product with ID {ProductId}.",id);

            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return false;

            await _repository.DeleteAsync(product);

            return true;
        }
    }
}