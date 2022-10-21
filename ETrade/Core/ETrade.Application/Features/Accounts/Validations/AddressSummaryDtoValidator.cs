using ETrade.Application.Features.Accounts.DTOs;
using FluentValidation;

namespace ETrade.Application.Features.Accounts.Validations;

public class AddressSummaryDtoValidator:AbstractValidator<AddressSummaryDto>
{
    public AddressSummaryDtoValidator()
    {
        RuleFor(address => address.AddressTitle)
            .NotNull()
            .WithMessage("Lütden adres başlığını boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden adres başlığını boş geçmeyiniz....")
            .MaximumLength(100)
            .WithMessage("En fazla 100 karakter girebilirsiniz...");
        
        RuleFor(address => address.AddressDetails)
            .NotNull()
            .WithMessage("Lütden detaylı adresi boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden detaylı adresi boş geçmeyiniz....")
            .MaximumLength(500)
            .WithMessage("En fazla 500 karakter girebilirsiniz...");
    }
}