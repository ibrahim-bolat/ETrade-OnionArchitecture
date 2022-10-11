using ETrade.Application.Features.Accounts.DTOs.UserDtos;
using FluentValidation;

namespace ETrade.Application.Features.Accounts.Validations.UserValidations;

public class LoginDtoValidator:AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .EmailAddress()
            .WithMessage("Lütfen uygun formatta e-mail adresi giriniz.");

        RuleFor(x => x.Password)
            .NotNull()
            .WithMessage("Lütfen şifenizi boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen şifrenizi boş geçmeyiniz...");
    }
}