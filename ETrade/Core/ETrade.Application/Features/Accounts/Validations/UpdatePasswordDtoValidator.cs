using ETrade.Application.Features.Accounts.DTOs;
using FluentValidation;

namespace ETrade.Application.Features.Accounts.Validations;

public class UpdatePasswordDtoValidator:AbstractValidator<UpdatePasswordDto>
{
    public UpdatePasswordDtoValidator()
    {
        RuleFor(x => x.Password)
            .NotNull()
            .WithMessage("Lütfen şifrenizi boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen şifrenizi boş geçmeyiniz...");


        RuleFor(x => x.RePassword)
            .NotNull()
            .WithMessage("Lütfen şifre tekrarı boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen şifre tekrarı boş geçmeyiniz...")
            .Equal(x => x.Password)
            .WithMessage("Lütfen şifre ile aynı giriniz...");
    }
}