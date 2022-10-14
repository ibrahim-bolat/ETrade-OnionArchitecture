using ETrade.Application.Features.Accounts.DTOs.UserDtos;
using MediatR;

namespace ETrade.Application.Features.Accounts.Commands.SetActiveUserCommand;

public class SetActiveUserCommandRequest:IRequest<SetActiveUserCommandResponse>
{
    public string Id { get; set; }
}