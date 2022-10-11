
using ETrade.Application.Features.UserImages.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.UserImages.Commands.DeleteUserImageCommand;

public class DeleteUserImageCommandResponse
{
    public IDataResult<UserImageDto> Result { get; set; }
}