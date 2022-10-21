using ETrade.Application.DTOs.Common;
using MediatR;

namespace ETrade.Application.Features.RoleOperations.Commands.CreateRoleCommand;

public class CreateRoleCommandRequest:IRequest<CreateRoleCommandResponse>
{
    public RoleDto RoleDto{ get; set; }
}