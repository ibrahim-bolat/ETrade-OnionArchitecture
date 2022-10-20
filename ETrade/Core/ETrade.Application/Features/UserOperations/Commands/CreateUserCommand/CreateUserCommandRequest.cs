using ETrade.Application.Features.UserOperations.DTOs;
using MediatR;

namespace ETrade.Application.Features.UserOperations.Commands.CreateUserCommand;

public class CreateUserCommandRequest:IRequest<CreateUserCommandResponse>
{
    public CreateUserDto CreateUserDto { get; set; }
}