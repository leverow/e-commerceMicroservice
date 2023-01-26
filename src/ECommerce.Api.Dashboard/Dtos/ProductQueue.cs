namespace ECommerce.Api.Dashboard.Dtos;

public class ProductQueue
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public Category? Category { get; set; }
    public Brand? Brand { get; set; }
    public int Count { get; set; }
}

public class Category
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class Brand
{ 
    public string? Name { get; set; }
    public string? Description { get; set; }
}
