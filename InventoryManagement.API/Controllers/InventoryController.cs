using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InventoryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create New Product")]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        var product = await _productService.CreateAsync(dto);

        return Ok(product);
    }

    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "GetAllProduct")]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllAsync();

        return Ok(products);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "GetProductById")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound("Product not found.");
        }

        return Ok(product);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "UpdateProduct")]
    public async Task<IActionResult> Update(int id, ProductDto dto)
    {
        var result = await _productService.UpdateAsync(id, dto);

        if (!result)
        {
            return NotFound("Product not found.");
        }

        return Ok("Product updated successfully.");
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "DeleteProduct")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _productService.DeleteAsync(id);

        if (!result)
        {
            return NotFound("Product not found.");
        }

        return Ok("Product deleted successfully.");
    }
}
