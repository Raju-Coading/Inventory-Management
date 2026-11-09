using InventoryManagement.API.Models;
using InventoryManagement.API.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InventoryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

       
        [HttpGet]
        [SwaggerOperation(Summary = "Get all product")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }

        
        [HttpGet("{id}")]

        [SwaggerOperation(Summary = "Get product By Id")]

        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _service.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Create product")]
        public async Task<ActionResult<Product>> CreateProduct(CreateProductDto productDto)
        {
           
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                StockQuantity = productDto.StockQuantity,
                CreatedAt = DateTime.UtcNow
            };

            var createdProduct = await _service.CreateAsync(product);

            return CreatedAtAction(
                nameof(GetProduct),
                new { id = createdProduct.Id },
                createdProduct);
        }


        [HttpPut("{id}")]

        [SwaggerOperation(Summary = "Update product by ID")]
        public async Task<IActionResult> UpdateProduct(
            int id,
            Product product)
        {
            var updated = await _service.UpdateAsync(id, product);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        
        [HttpDelete("{id}")]

        [SwaggerOperation(Summary = "Delete product by ID")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}