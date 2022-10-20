using ETrade.Application.Features.Accounts.DTOs;
using FluentValidation;

namespace ETrade.Application.Features.Accounts.Validations.UserValidations;

public class EditPasswordDtoValidator:AbstractValidator<EditPasswordDto>
{
    public EditPasswordDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Lütfen adınızı boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen adınızı boş geçmeyiniz...");
            
        RuleFor(x => x.UserName)
            .NotNull()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .MaximumLength(30)
            .WithMessage("En fazla 30 karakter girebilirsiniz...");
        
        RuleFor(x => x.NewPassword)
            .NotNull()
            .WithMessage("Lütfen yeni şifrenizi boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen yeni şifrenizi boş geçmeyiniz...");

        RuleFor(x => x.ReNewPassword)
            .NotNull()
            .WithMessage("Lütfen tekrar yeni şifrenizi boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen tekrar yeni şifrenizi boş geçmeyiniz...")
            .Equal(x => x.NewPassword)
            .WithMessage("Lütfen yeni şifre ile aynı giriniz...");
    }
}