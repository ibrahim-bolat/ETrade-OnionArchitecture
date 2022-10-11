using ETrade.Application.Features.Accounts.DTOs.RoleDtos;
using FluentValidation;

namespace ETrade.Application.Features.Accounts.Validations.RoleValidations;

public class RoleAssignDtoValidator:AbstractValidator<RoleAssignDto>
{
    public RoleAssignDtoValidator()
    {
        
    }
}