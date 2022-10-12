using MediatR;

namespace ETrade.Application.Features.Accounts.Commands.VerifyTokenUserCommand;

public class VerifyTokenUserCommandRequest:IRequest<VerifyTokenUserCommandResponse>
{
    public string UserId { get; set; }
    public string Token { get; set; }
}