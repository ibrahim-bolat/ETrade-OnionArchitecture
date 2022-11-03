using MediatR;

namespace ETrade.Application.Features.RoleOperations.Commands.SetRoleActiveCommand;

public class SetRoleActiveCommandRequest:IRequest<SetRoleActiveCommandResponse>
{
    public string Id { get; set; }
}