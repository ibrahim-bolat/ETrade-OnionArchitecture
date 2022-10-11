using ETrade.Application.Features.Accounts.DTOs.RoleDtos;
using FluentValidation;

namespace ETrade.Application.Features.Accounts.Validations.RoleValidations;

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