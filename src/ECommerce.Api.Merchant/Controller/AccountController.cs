using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ECommerce.Api.Merchant.Services;

namespace ECommerce.Api.Merchant.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(string username, string email, [FromServices] IJwtService jwtTokenService)
        {
            return Ok(jwtTokenService.GenerateToken(username, email));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Profile()
        {
            return Ok(User.FindFirst(ClaimTypes.Name)!.Value);
        }
    }
}
