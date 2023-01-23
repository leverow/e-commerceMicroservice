namespace ECommerce.Api.Cart.Entities;

public class Cart
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public uint Count { get; set; }
    public string? Properties { get; set; }
}