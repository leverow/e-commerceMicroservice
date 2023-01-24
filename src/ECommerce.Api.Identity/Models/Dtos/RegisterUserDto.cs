namespace ECommerce.Api.Identity.Models.Dtos;

public class RegisterUserDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public IFormFile? Image { get; set; }
}