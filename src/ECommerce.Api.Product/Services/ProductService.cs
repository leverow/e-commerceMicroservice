using ECommerce.Api.Product.Context;
using ECommerce.Api.Product.Entities;
using MongoDB.Driver;

namespace ECommerce.Api.Product.Services;

public class ProductService
{
    private ECommerceDbContext _context;

    public ProductService(ECommerceDbContext context)
	{
		_context = context;
	}

    public async Task<List<Entities.Product>> GetProducts()
    {
        var products = await (await _context.Products.FindAsync(products => true)).ToListAsync();

        return products;
    }
    public async Task<Entities.Product> GetProductById()
    {
        var products = await (await _context.Products.FindAsync(products => true)).ToListAsync();

        return products;
    }

    public async Task<Entities.Product?> CreateProduct(Entities.Product product)
	{
		if (product is null)
			return product;

        await _context.Products.InsertOneAsync(product);

		return product;
    }
}

