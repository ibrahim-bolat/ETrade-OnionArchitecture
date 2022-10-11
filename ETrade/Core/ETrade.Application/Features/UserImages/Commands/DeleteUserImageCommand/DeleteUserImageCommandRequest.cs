using MediatR;

namespace ETrade.Application.Features.UserImages.Commands.DeleteUserImageCommand;

public class DeleteUserImageCommandRequest:IRequest<DeleteUserImageCommandResponse>
{
    public int Id { get; set; }
    public string ModifiedByName { get; set; }
}