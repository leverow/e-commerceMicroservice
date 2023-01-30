namespace ECommerce.Api.Merchant.Options;

public class JwtTokenValidationParameters
{
    public string? ValidIssuer { get; set; }
    public string? ValidAudience { get; set; }
    public string IssuerSigningKey { get; set; } = string.Empty;
}