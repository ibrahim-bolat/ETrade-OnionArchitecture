using MediatR;

namespace ETrade.Application.Features.UserOperations.Commands.SetActiveUserCommand;

public class SetActiveUserCommandRequest:IRequest<SetActiveUserCommandResponse>
{
    public string Id { get; set; }
}