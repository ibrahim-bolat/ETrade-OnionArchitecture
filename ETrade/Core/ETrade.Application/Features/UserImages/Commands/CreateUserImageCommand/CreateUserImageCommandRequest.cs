using ETrade.Application.Features.UserImages.DTOs;
using MediatR;

namespace ETrade.Application.Features.UserImages.Commands.CreateUserImageCommand;

public class CreateUserImageCommandRequest:IRequest<CreateUserImageCommandResponse>
{
    public CreateUserImageDto UserImageAddDto { get; set; }
    public string CreatedByName { get; set; }
}