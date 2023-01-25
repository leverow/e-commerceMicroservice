using ECommerce.Api.Cart.Dtos;

namespace ECommerce.Api.Cart.Services;

public interface ICartService
{
    Task<List<Entities.Cart>?> GetCart(string key);
    Task<Entities.Cart> AddCart(string key, CreateProductDto createProductDto);
    Task<Entities.Cart> UpdateCart(string key, UpdateProductDto updateProductDto);
    Task<Entities.Cart> DeleteCart(string key, string cartId);
}