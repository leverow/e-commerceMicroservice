using ECommerce.Api.Cart.Dtos;
using Mapster;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace ECommerce.Api.Cart.Services;

public class CartService : ICartService
{
    private readonly IDistributedCache _distributedCache;

    public CartService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<List<Entities.Cart>?> GetCart(string key)
    {
        var valuesJson = await _distributedCache.GetStringAsync(key);

        if (valuesJson == null)
            return null;

        return JsonConvert.DeserializeObject<List<Entities.Cart>>(valuesJson);

    }

    public async Task<Entities.Cart> AddCart(string key, CreateProductDto createProductDto)
    {
        var productsJson = await _distributedCache.GetStringAsync(key);
        var products = JsonConvert.DeserializeObject<List<Entities.Cart>>(productsJson ?? string.Empty);
        var product = createProductDto.Adapt<Entities.Cart>();

        products ??= new List<Entities.Cart>();

        products.Add(product);
        await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(products), new DistributedCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromDays(10)
        });
        return product;
    }

    public async Task<Entities.Cart> UpdateCart(string key, UpdateProductDto updateProductDto)
    {
        var productsJson = await _distributedCache.GetStringAsync(key);


        var products = JsonConvert.DeserializeObject<List<Entities.Cart>>(productsJson);
        var product = products!.FirstOrDefault(product => product.Id == updateProductDto.Id);


        products!.Remove(product);
        products.Add(updateProductDto.Adapt<Entities.Cart>());

        await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(products), new DistributedCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromDays(10)
        });
        return product;
    }

    public async Task<Entities.Cart> DeleteCart(string key, string cartId)
    {
        var productsJson = await _distributedCache.GetStringAsync(key).ConfigureAwait(false);

        var products = JsonConvert.DeserializeObject<List<Entities.Cart>>(productsJson);
        var product = products!.FirstOrDefault(cart => cart.Id == cartId);

        products.Remove(product!);

        return product;
    }
}