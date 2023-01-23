using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.Api.Product.Entities;

public class Product
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public Category? Category { get; set; }
    public Brand? Brand { get; set; }
    public int Count { get; set; }
}