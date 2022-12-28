using ETrade.Application.DTOs;
using MediatR;

namespace ETrade.Application.Features.UserOperations.Commands.EditPasswordUserCommand;

public class EditPasswordUserCommandRequest:IRequest<EditPasswordUserCommandResponse>
{
    public EditPasswordDto EditPasswordDto { get; set; }
}