using ETrade.Application.Features.Accounts.DTOs;
using MediatR;

namespace ETrade.Application.Features.Accounts.Commands.UpdatePasswordUserCommand;

public class UpdatePasswordUserCommandRequest:IRequest<UpdatePasswordUserCommandResponse>
{
    public UpdatePasswordDto UpdatePasswordDto { get; set; }
    public string UserId { get; set; }
    public string Token { get; set; }
}