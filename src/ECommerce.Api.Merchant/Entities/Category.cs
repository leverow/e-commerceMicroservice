namespace ECommerce.Api.Merchant.Entities;

public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Views { get; set; }
    public int ParentId { get; set; }
}