using ECommerce.Api.Product.Context;
using ECommerce.Api.Product.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ECommerce.Api.Product.Services;

public class ProductService : IProductService
{
    private readonly ECommerceDbContext _dbContext;

    public ProductService(ECommerceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Entities.Product>> GetAllProducts()
    {
        return await _dbContext.Products.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<Entities.Product> GetProductById(string id)
    {
        var filter = Builders<Entities.Product>.Filter.Eq(p => p.Id, id);
        return await _dbContext.Products.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<Entities.Product> CreateProduct(Entities.Product product)
    {
        await _dbContext.Products.InsertOneAsync(product);
        return product;
    }

    public async Task<Entities.Product> UpdateProduct(string id, Entities.Product product)
    {
        var filter = Builders<Entities.Product>.Filter.Eq(p => p.Id, id);
        await _dbContext.Products.ReplaceOneAsync(filter, product);
        return product;
    }
    public async Task DeleteProduct(string id)
    {
        var filter = Builders<Entities.Product>.Filter.Eq(p => p.Id, id);
        await _dbContext.Products.DeleteOneAsync(filter);
    }
}
