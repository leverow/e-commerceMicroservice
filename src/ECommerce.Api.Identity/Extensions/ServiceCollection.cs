using ECommerce.Api.Identity.Data;
using ECommerce.Api.Identity.Models;

namespace ECommerce.Api.Identity.Extensions;

public static class ServiceCollection
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredUniqueChars = 0;
        }).AddEntityFrameworkStores<AppDbContext>();
        return services;
    }
}