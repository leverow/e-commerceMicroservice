using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace ECommerce.Api.Cart.Controller;

[Route("api/[controller]")]
[ApiController]
public class CartsController : ControllerBase
{
    private readonly IDistributedCache _distributedCache;

    public CartsController(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(string key, string value)
    {
        var valuesJson = await _distributedCache.GetStringAsync(key);
        var values = new List<string>();

        if (valuesJson is not null)
            values = JsonConvert.DeserializeObject<List<string>>(valuesJson);

        values!.Add(value);

        await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(values), new DistributedCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromDays(10)
        });

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetCart(string key)
    {
        var valuesJson = await _distributedCache.GetStringAsync(key);

        if (valuesJson is null)
            return Ok(new List<string>());

        var values = JsonConvert.DeserializeObject<List<string>>(valuesJson);

        return Ok(values);
    }
}