using ECommerce.Api.Identity.Models;
using ECommerce.Api.Identity.Models.Dtos;
using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Identity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IValidator<LoginUserDto> loginUserValidator;
    private readonly IValidator<RegisterUserDto> registerUserValidator;
    private readonly SignInManager<AppUser> signInManager;
    private readonly UserManager<AppUser> userManager;
    
    public AccountController(
        IValidator<LoginUserDto> loginUserValidator,
        SignInManager<AppUser> signInManager,
        IValidator<RegisterUserDto> registerUserValidator,
        UserManager<AppUser> userManager)
    {
        this.loginUserValidator = loginUserValidator;
        this.signInManager = signInManager;
        this.registerUserValidator = registerUserValidator;
        this.userManager = userManager;
    }

    [HttpPost("login")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(Result<string>), 404)]
    [ProducesResponseType(typeof(Result<List<ValidationFailure>>), 400)]
    public async Task<IActionResult> SignIn([FromBody]LoginUserDto dto)
    {
        var validateResult = loginUserValidator.Validate(dto);
        if (!validateResult.IsValid) return BadRequest(new Result<List<ValidationFailure>>(validateResult.Errors));
        
        var signInResult = await signInManager.PasswordSignInAsync(dto.UserName!,dto.Password,isPersistent: true, lockoutOnFailure: false);
        
        if (!signInResult.Succeeded)
            return NotFound(new Result<string>("User authorization failed"));
        
        return Ok();
    }

    [HttpPost("register")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(Result<List<ValidationFailure>>), 400)]
    [ProducesResponseType(typeof(Result<IEnumerable<IdentityError>>), 400)]
    public async Task<IActionResult> SignUp([FromBody]RegisterUserDto dto)
    {
        var validateResult = registerUserValidator.Validate(dto);
        if (!validateResult.IsValid) return BadRequest(new Result<List<ValidationFailure>>(validateResult.Errors));
        
        var user = dto.Adapt<AppUser>();
        
        //TODO: dto.Image ni Files.Apiga gRPC orqali bog'lab saqlash kerak va faylni nomi va extension'ini saqlash kerak
        
        var createdUserResult = await userManager.CreateAsync(user, dto.Password);
        
        if (!createdUserResult.Succeeded)
            return BadRequest( new Result<IEnumerable<IdentityError>>(createdUserResult.Errors));

        await signInManager.SignInAsync(user, true);

        return Ok();
    }

    [HttpGet("logout")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> SignOut()
    {
        await signInManager.SignOutAsync();
        return Ok();
    }

    private record Result<T>(T? Error);
}