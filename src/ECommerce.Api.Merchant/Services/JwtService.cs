using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using ECommerce.Api.Merchant.Options;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Api.Merchant.Services;

public class JwtService : IJwtService
{
    private readonly JwtTokenValidationParameters _validationParameters;

    public JwtService(IOptions<JwtTokenValidationParameters> validationParameters)
    {
        _validationParameters = validationParameters.Value;
    }
    public string GenerateToken(string username, string email)
    {
        var keyByte = System.Text.Encoding.UTF8.GetBytes(_validationParameters.IssuerSigningKey);
        var securityKey = new SigningCredentials(new SymmetricSecurityKey(keyByte), SecurityAlgorithms.HmacSha256);

        var security = new JwtSecurityToken(
            issuer: _validationParameters.ValidIssuer,
            audience: _validationParameters.ValidAudience,
            new Claim[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, username)
            },
            expires: DateTime.Now.AddMinutes(20),
            signingCredentials: securityKey);

        return new JwtSecurityTokenHandler().WriteToken(security);
    }
}