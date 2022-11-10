using MediatR;

namespace ETrade.Application.Features.RoleOperations.Commands.SetRoleActiveCommand;

public class SetRoleActiveCommandRequest:IRequest<SetRoleActiveCommandResponse>
{
    public int Id { get; set; }
}