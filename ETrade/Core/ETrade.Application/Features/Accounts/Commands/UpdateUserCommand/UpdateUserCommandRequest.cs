using ETrade.Application.Features.Accounts.DTOs.UserDtos;
using MediatR;

namespace ETrade.Application.Features.Accounts.Commands.UpdateUserCommand;

public class UpdateUserCommandRequest:IRequest<UpdateUserCommandResponse>
{
    public UserDto UserDto { get; set; }
    public string OldEmail { get; set; }
}