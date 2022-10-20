using MediatR;

namespace ETrade.Application.Features.RoleOperations.Commands.SetDeletedRoleCommand;

public class SetDeletedRoleCommandRequest:IRequest<SetDeletedRoleCommandResponse>
{
    public string Id { get; set; }
}