using ETrade.Application.DTOs.RoleDtos;
using FluentValidation;

namespace ETrade.Application.Validations.FluentValidation.Account;

public class RoleDtoValidator:AbstractValidator<RoleDto>
{
    public RoleDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("Lütden rolü boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden rolü boş geçmeyiniz....");
    }
}