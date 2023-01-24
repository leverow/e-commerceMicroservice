using Microsoft.AspNetCore.Identity;

namespace ECommerce.Api.Identity.Models;

public class AppUser : IdentityUser<Guid>
{
    public string? ImageUrl { get; set; }
}