using MediatR;

namespace ETrade.Application.Features.RoleOperations.Commands.SetRolePassiveCommand;

public class SetRolePassiveCommandRequest:IRequest<SetRolePassiveCommandResponse>
{
    public string Id { get; set; }
}