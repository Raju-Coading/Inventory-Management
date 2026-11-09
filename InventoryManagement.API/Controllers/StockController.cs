using InventoryManagement.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace InventoryManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }
        [HttpGet("{productId}")]
        [SwaggerOperation(Summary = "Get all product Stock")]


        public async Task<IActionResult> GetStock(int productId)
        {
            var stock = await _stockService.GetStockAsync(productId);

            if (stock == null)
                return NotFound("Product not found.");

            return Ok(new
            {
                ProductId = productId,
                StockQuantity = stock
            });
        }

     
        [HttpPost("{productId}/add")]
        [SwaggerOperation(Summary = "Add New product Stock")]
        public async Task<IActionResult> AddStock(
            int productId,
            [FromQuery] int quantity)
        {
            var result = await _stockService.AddStockAsync(
                productId,
                quantity);

            if (!result)
                return BadRequest(
                    "Product not found or quantity is invalid.");
        
            return Ok(new
            {
                Message = "Stock added successfully."
            });
        }

        [HttpPost("{productId}/reduce")]
        [SwaggerOperation(Summary = "Reduce product Stock")]
        public async Task<IActionResult> ReduceStock(
            int productId,
            [FromQuery] int quantity)
        {
            var result = await _stockService.ReduceStockAsync(
                productId,
                quantity);

            if (!result)
                return BadRequest(
                    "Product not found, quantity is invalid, or stock is insufficient.");

            return Ok(new
            {
                Message = "Stock reduced successfully."
            });
        }
    }
}