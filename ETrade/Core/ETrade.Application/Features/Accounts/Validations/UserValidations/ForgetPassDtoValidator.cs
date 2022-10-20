using ETrade.Application.Features.Accounts.DTOs;
using FluentValidation;

namespace ETrade.Application.Features.Accounts.Validations.UserValidations;

public class ForgetPassDtoValidator:AbstractValidator<ForgetPassDto>
{
    public ForgetPassDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .EmailAddress()
            .WithMessage("Lütfen uygun formatta e-mail adresi giriniz.");

    }
}