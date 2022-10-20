using ETrade.Application.Features.UserOperations.DTOs;
using FluentValidation;

namespace ETrade.Application.Features.UserOperations.Validations;

public class RoleAssignDtoValidator:AbstractValidator<RoleAssignDto>
{
    public RoleAssignDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("Lütfen Rolü boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen Rolü boş geçmeyiniz...")
            .MaximumLength(100)
            .WithMessage("En fazla 100 karakter girebilirsiniz...");
    }
}