using ECommerce.Api.Cart.Dtos;
using ECommerce.Api.Cart.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace ECommerce.Api.Cart.Controller;

[Route("api/[controller]")]
[ApiController]
public class CartsController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly IDistributedCache _distributedCache;

    public CartsController(IDistributedCache distributedCache, ICartService cartService)
    {
        _distributedCache = distributedCache;
        _cartService = cartService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddProduct(string key, CreateProductDto createProductDto)
    {
        var productcart = await _cartService.AddCart(key, createProductDto);
        return Ok(productcart);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Entities.Cart>))]
    public async Task<IActionResult> GetCart(string key)
    {
        var products = await _cartService.GetCart(key);

        return Ok(products ?? new List<Entities.Cart>());
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProduct(string key, UpdateProductDto updateProductDto)
    {
        var productcarts = await _cartService.UpdateCart(key, updateProductDto);

        return Ok(productcarts);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Entities.Cart))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteProduct(string key, string cartId)
    {
        var productCart = await _cartService.DeleteCart(key, cartId);
        if (productCart is null)
            return BadRequest();

        return Ok();
    }
}