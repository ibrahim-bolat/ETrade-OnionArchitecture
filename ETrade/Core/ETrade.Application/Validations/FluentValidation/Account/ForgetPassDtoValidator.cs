using ETrade.Application.DTOs.UserDtos;
using FluentValidation;

namespace ETrade.Application.Validations.FluentValidation.Account;

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