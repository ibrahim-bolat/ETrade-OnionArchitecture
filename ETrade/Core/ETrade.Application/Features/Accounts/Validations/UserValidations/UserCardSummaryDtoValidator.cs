using ETrade.Application.Features.Accounts.DTOs;
using FluentValidation;

namespace ETrade.Application.Features.Accounts.Validations.UserValidations;

public class UserCardSummaryDtoValidator:AbstractValidator<UserSummaryCardDto>
{
    public UserCardSummaryDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotNull()
            .WithMessage("Lütfen adınızı boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen adınızı boş geçmeyiniz...")
            .MaximumLength(100)
            .WithMessage("En fazla 100 karakter girebilirsiniz...");
        
        RuleFor(x => x.LastName)
            .NotNull()
            .WithMessage("Lütfen soyadınızı boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen soyadınızı boş geçmeyiniz...")
            .MaximumLength(100)
            .WithMessage("En fazla 100 karakter girebilirsiniz...");
        
        RuleFor(x => x.UserName)
            .NotNull()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .MaximumLength(30)
            .WithMessage("En fazla 30 karakter girebilirsiniz...");

        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .EmailAddress()
            .WithMessage("Lütfen uygun formatta e-mail adresi giriniz.");
        
        RuleFor(address => address.DefaultAddressDetail)
            .NotNull()
            .WithMessage("Lütden detaylı adresi boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden detaylı adresi boş geçmeyiniz....")
            .MaximumLength(500)
            .WithMessage("En fazla 500 karakter girebilirsiniz...");
        
    }
}