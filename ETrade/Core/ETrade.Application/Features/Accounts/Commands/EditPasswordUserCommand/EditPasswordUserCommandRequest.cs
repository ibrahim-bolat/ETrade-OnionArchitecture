using ETrade.Application.Features.Accounts.DTOs.UserDtos;
using MediatR;

namespace ETrade.Application.Features.Accounts.Commands.EditPasswordUserCommand;

public class EditPasswordUserCommandRequest:IRequest<EditPasswordUserCommandResponse>
{
    public EditPasswordDto EditPasswordDto { get; set; }
}