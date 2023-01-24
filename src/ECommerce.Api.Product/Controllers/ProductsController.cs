using ECommerce.Api.Product.Dtos;
using ECommerce.Api.Product.Services;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace ECommerce.Api.Product.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(string id)
    {
        var product = await _service.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        return Ok(await _service.GetAllProducts());
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
    {
        var product = productDto.Adapt<Entities.Product>(); 

        await _service.CreateProduct(product);
        return CreatedAtAction("GetProductById", new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(string id, [FromBody] ProductDto productDto)
    {
        var product = await _service.GetProductById(id);

        if (product == null)
            return BadRequest();
      
        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.Price = productDto.Price;
        product.ImageUrl = productDto.ImageUrl;
        product.Category.Name = productDto.Category.Name;
        product.Category.Description = productDto.Category.Description;
        product.Brand.Name = productDto.Brand.Name;
        product.Brand.Description = productDto.Brand.Description;

        await _service.UpdateProduct(id, product);
        return Ok(productDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        await _service.DeleteProduct(id);
        return Ok();
    }
}
