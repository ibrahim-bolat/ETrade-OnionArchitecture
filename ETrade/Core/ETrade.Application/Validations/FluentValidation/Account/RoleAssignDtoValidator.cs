using ETrade.Application.DTOs.RoleDtos;
using FluentValidation;

namespace ETrade.Application.Validations.FluentValidation.Account;

public class RoleAssignDtoValidator:AbstractValidator<RoleAssignDto>
{
    public RoleAssignDtoValidator()
    {
        
    }
}