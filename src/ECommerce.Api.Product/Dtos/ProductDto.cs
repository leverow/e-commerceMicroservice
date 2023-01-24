
namespace ECommerce.Api.Product.Dtos;

public class ProductDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public CategoryDto? Category { get; set; }
    public BrandDto? Brand { get; set; }
    public int Count { get; set; }
}
