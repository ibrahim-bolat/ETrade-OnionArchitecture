using ETrade.Application.Features.UserOperations.DTOs;
using MediatR;

namespace ETrade.Application.Features.UserOperations.Commands.EditPasswordUserCommand;

public class EditPasswordUserCommandRequest:IRequest<EditPasswordUserCommandResponse>
{
    public EditPasswordUserDto EditPasswordUserDto { get; set; }
}