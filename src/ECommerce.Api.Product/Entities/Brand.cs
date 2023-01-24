namespace ECommerce.Api.Product.Entities;

public class Brand
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Name { get; set; }
    public string? Description { get; set; }
}
