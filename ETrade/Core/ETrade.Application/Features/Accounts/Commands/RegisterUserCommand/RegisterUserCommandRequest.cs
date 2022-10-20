using ETrade.Application.Features.Accounts.DTOs;
using MediatR;

namespace ETrade.Application.Features.Accounts.Commands.RegisterUserCommand;

public class RegisterUserCommandRequest:IRequest<RegisterUserCommandResponse>
{
    public RegisterDto RegisterDto { get; set; }
}