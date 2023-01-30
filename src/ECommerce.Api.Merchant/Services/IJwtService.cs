namespace ECommerce.Api.Merchant.Services;

public interface IJwtService
{
    string GenerateToken(string username, string email);
}