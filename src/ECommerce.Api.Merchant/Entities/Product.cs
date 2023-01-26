namespace ECommerce.Api.Merchant.Entities;

public class Product
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}