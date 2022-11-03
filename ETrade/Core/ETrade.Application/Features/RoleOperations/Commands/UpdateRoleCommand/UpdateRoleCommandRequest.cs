using ETrade.Application.Features.RoleOperations.DTOs;
using MediatR;

namespace ETrade.Application.Features.RoleOperations.Commands.UpdateRoleCommand;

public class UpdateRoleCommandRequest:IRequest<UpdateRoleCommandResponse>
{
    public RoleDto RoleDto{ get; set; }
}