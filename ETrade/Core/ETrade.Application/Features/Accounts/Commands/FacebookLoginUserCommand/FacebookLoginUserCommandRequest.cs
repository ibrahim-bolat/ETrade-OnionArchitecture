using MediatR;

namespace ETrade.Application.Features.Accounts.Commands.FacebookLoginUserCommand;

public class FacebookLoginUserCommandRequest:IRequest<FacebookLoginUserCommandResponse>
{
    public bool IsPersistent { get; set; }
}