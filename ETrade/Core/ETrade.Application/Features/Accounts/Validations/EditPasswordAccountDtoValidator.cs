using ETrade.Application.Features.Accounts.DTOs;
using FluentValidation;

namespace ETrade.Application.Features.Accounts.Validations;

public class EditPasswordAccountDtoValidator:AbstractValidator<EditPasswordAccountDto>
{
    public EditPasswordAccountDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Lütfen adınızı boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen adınızı boş geçmeyiniz...");
            
        RuleFor(x => x.UserName)
            .NotNull()
            .WithMessage("Lütfen kullanıcı adını boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen kullanıcı adını boş geçmeyiniz...")
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