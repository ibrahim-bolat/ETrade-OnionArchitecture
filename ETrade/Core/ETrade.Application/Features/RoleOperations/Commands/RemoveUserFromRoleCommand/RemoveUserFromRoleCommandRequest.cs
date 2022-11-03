using MediatR;

namespace ETrade.Application.Features.RoleOperations.Commands.RemoveUserFromRoleCommand;

public class RemoveUserFromRoleCommandRequest:IRequest<RemoveUserFromRoleCommandResponse>
{
    public string UserId { get; set; }
    public string RoleId { get; set; }
}