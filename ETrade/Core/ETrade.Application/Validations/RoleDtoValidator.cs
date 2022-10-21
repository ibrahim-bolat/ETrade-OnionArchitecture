using ETrade.Application.DTOs.Common;
using FluentValidation;

namespace ETrade.Application.Validations;

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