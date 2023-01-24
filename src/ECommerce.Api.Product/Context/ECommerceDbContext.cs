using ECommerce.Api.Product.Entities;
using MongoDB.Driver;


namespace ECommerce.Api.Product.Context;

public class ECommerceDbContext
{
    private readonly MongoClient _mongoClient;
    private readonly IMongoDatabase _database;
    private readonly IConfiguration _config;

    public ECommerceDbContext(IConfiguration configuration)
    {
        _config = configuration.GetSection("MongoDbSettings");

        _mongoClient = new MongoClient(_config["MongoClient"]);
        _database = _mongoClient.GetDatabase(_config["Database"]); 
    }

    public IMongoCollection<Entities.Product> Products
    {
        get
        {
            return _database.GetCollection<Entities.Product>("products");
        }
    }
}