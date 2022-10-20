using ETrade.Application.Features.Accounts.DTOs;
using MediatR;

namespace ETrade.Application.Features.Accounts.Commands.LoginUserCommand;

public class LoginUserCommandRequest:IRequest<LoginUserCommandResponse>
{
    public LoginDto LoginDto { get; set; }
}