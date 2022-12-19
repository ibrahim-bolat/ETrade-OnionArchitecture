using MediatR;

namespace ETrade.Application.Features.Accounts.Commands.ExternalLoginUserCommand;

public class ExternalLoginUserCommandRequest:IRequest<ExternalLoginUserCommandResponse>
{
    public bool IsPersistent { get; set; }
}