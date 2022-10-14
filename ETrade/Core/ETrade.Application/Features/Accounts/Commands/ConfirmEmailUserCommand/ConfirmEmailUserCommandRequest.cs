using MediatR;

namespace ETrade.Application.Features.Accounts.Commands.ConfirmEmailUserCommand;

public class ConfirmEmailUserCommandRequest:IRequest<ConfirmEmailUserCommandResponse>
{
    public string Email { get; set; }
    public string Token { get; set; }
}