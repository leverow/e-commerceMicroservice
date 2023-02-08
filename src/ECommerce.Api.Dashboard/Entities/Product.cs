namespace ECommerce.Api.Dashboard.Entities;

public class Product
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public decimal ProductPrice { get; set; }
    public string? ProductImageUrl { get; set; }
    public string? ProductCategory { get; set; }
    public string? ProductBrand { get; set; }
    public int ProductCount { get; set; }
}