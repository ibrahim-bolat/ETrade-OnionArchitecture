using MediatR;

namespace ETrade.Application.Features.RoleOperations.Commands.SetRolePassiveCommand;

public class SetRolePassiveCommandRequest:IRequest<SetRolePassiveCommandResponse>
{
    public int Id { get; set; }
}