using ECommerce.Api.Identity.Models.Dtos;
using FluentValidation;

namespace ECommerce.Api.Identity.Validators;

public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
{
    public LoginUserDtoValidator()
    {
        RuleFor(loginUserDto => loginUserDto.UserName).NotNull().When(loginUserDto => loginUserDto.Email == null);
        RuleFor(loginUserDto => loginUserDto.Email).NotNull().When(loginUserDto => loginUserDto.UserName == null);
        RuleFor(loginUserDto => loginUserDto.Password).NotNull();
    }
}