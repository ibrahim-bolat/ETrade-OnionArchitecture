using System.Net;
using ETrade.Application.Features.IpOperations.DTOs;
using ETrade.Application.Features.RoleOperations.DTOs;
using FluentValidation;

namespace ETrade.Application.Features.IpOperations.Validations;

public class IpDtoValidator:AbstractValidator<IpDto>
{
    public IpDtoValidator()
    {
        RuleFor(ip => ip.RangeStart)
            .NotNull()
            .WithMessage("Lütden Ip Aralık Başlangıcını boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden Ip Aralık Başlangıcını boş geçmeyiniz....")
            .Must(isValidIp)
            .WithMessage("Lütfen Ip Adresini Doğru Giriniz....");
        RuleFor(ip => ip.RangeEnd)
            .NotNull()
            .WithMessage("Lütden Ip Aralık Sonunu boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden Ip Aralık Sonunu boş geçmeyiniz....")
            .Must(isValidIp)
            .WithMessage("Lütfen Ip Adresini Doğru Giriniz....");
        RuleFor(ip => ip.IpListType)
            .NotNull()
            .WithMessage("Lütden Ip Listesi tipini boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden Ip Listesi tipini boş geçmeyiniz....");
    }
    private bool isValidIp(string ipAddress)
    {
        IPAddress address;
        return IPAddress.TryParse(ipAddress, out address) && address.ToString() == ipAddress;
    }
}