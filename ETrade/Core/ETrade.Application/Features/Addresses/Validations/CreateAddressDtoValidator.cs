using System.Text.RegularExpressions;
using ETrade.Application.Features.Addresses.DTOs;
using FluentValidation;

namespace ETrade.Application.Features.Addresses.Validations;

public class CreateAddressDtoValidator:AbstractValidator<CreateAddressDto>
{
    public CreateAddressDtoValidator()
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
        
        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .EmailAddress()
            .WithMessage("Lütfen uygun formatta e-mail adresi giriniz.");

        RuleFor(x => x.PhoneNumber)
            .NotNull()
            .WithMessage("Lütfen telefonu boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen telefonu boş geçmeyiniz...")
            .MinimumLength(17).WithMessage("Telefon en az 17 karakter olabilir.")
            .MaximumLength(17).WithMessage("Telefon en fazla 17 karakter olabilir.")
            .Matches(new Regex(@"^((\+90))\(?([0-9]{3})\)?([0-9]{3})[-]?([0-9]{2})[-]?([0-9]{2})$"))
            .WithMessage("Lütfen uygun formatta telefon giriniz.");

        RuleFor(address => address.AddressTitle)
            .NotNull()
            .WithMessage("Lütden adres başlığını boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden adres başlığını boş geçmeyiniz....")
            .MaximumLength(100)
            .WithMessage("En fazla 100 karakter girebilirsiniz...");

        RuleFor(address => address.AddressType)
            .NotNull()
            .WithMessage("Lütden adres tipini boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden adres tipini boş geçmeyiniz....");
        
        
        RuleFor(address => address.NeighborhoodOrVillageId)
            .NotNull()
            .WithMessage("Lütden mahalle yada köyü boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden mahalle yada köyü boş geçmeyiniz....")
            .MaximumLength(500)
            .WithMessage("En fazla 500 karakter girebilirsiniz...");
        
        RuleFor(address => address.DistrictId)
            .NotNull()
            .WithMessage("Lütden ilçeyi boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden ilçeyi boş geçmeyiniz....")
            .MaximumLength(250)
            .WithMessage("En fazla 250 karakter girebilirsiniz...");
        
        RuleFor(address => address.CityId)
            .NotNull()
            .WithMessage("Lütden ili boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden ili boş geçmeyiniz....")
            .MaximumLength(250)
            .WithMessage("En fazla 250 karakter girebilirsiniz...");


        RuleFor(address => address.PostalCode)
            .NotNull()
            .WithMessage("Lütfen posta kodunu boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen posta kodunu boş geçmeyiniz...")
            .MinimumLength(5).WithMessage("Lütfen posta kodunu 5 karakter olarak giriniz...")
            .MaximumLength(5).WithMessage("Lütfen posta kodunu 5 karakter olarak giriniz...");

        RuleFor(address => address.AddressDetails)
            .NotNull()
            .WithMessage("Lütden detaylı adresi boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden detaylı adresi boş geçmeyiniz....")
            .MaximumLength(500)
            .WithMessage("En fazla 500 karakter girebilirsiniz...");
        
        RuleFor(address => address.Note)
            .MaximumLength(500)
            .WithMessage("En fazla 500 karakter girebilirsiniz...");
    }
}