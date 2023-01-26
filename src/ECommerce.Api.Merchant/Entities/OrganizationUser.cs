using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Api.Merchant.Entities;

public class OrganizationUser
{
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual Users? User { get; set; }
    public Guid OrganizationId { get; set; }
    [ForeignKey(nameof(OrganizationId))]
    public virtual Organization Organization { get; set; }
    public ERole Role { get; set; }
}