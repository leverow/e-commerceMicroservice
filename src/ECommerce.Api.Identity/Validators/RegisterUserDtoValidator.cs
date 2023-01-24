using ECommerce.Api.Identity.Models.Dtos;
using FluentValidation;

namespace ECommerce.Api.Identity.Validators;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator()
    {
        RuleFor(registerUserDto => registerUserDto.UserName).NotNull();
        RuleFor(registerUserDto => registerUserDto.Email).NotNull();
        RuleFor(registerUserDto => registerUserDto.Password).NotNull();
    }
}