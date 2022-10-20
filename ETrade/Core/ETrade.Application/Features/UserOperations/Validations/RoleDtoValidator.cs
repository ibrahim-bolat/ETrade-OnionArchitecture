using ETrade.Application.Features.UserOperations.DTOs;
using FluentValidation;

namespace ETrade.Application.Features.UserOperations.Validations;

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