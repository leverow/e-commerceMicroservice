namespace ECommerce.Api.Merchant.Entities;

public class Organization
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public EOrganizationStatus Status { get; set; }
}