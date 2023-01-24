using MongoDB.Bson;

namespace ECommerce.Api.Product.Services;

public interface IProductService
{
    Task<Entities.Product> GetProductById(string id);
    Task<IEnumerable<Entities.Product>> GetAllProducts();
    Task<Entities.Product> CreateProduct(Entities.Product product);
    Task<Entities.Product> UpdateProduct(string id, Entities.Product product);
    Task DeleteProduct(string id);
}
