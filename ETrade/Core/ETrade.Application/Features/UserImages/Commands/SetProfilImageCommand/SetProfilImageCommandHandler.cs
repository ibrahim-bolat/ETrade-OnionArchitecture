using ETrade.Application.Features.UserImages.Constants;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;

namespace ETrade.Application.Features.UserImages.Commands.SetProfilImageCommand;

public class SetProfilImageCommandHandler:IRequestHandler<SetProfilImageCommandRequest,SetProfilImageCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
  
    public SetProfilImageCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }

    public async Task<SetProfilImageCommandResponse> Handle(SetProfilImageCommandRequest request, CancellationToken cancellationToken)
    {
        var userImages = await _unitOfWork.GetRepository<UserImage>().GetAllAsync(predicate:ui=>ui.UserId==request.UserId && ui.IsActive);
        if (userImages !=null)
        {
            foreach (var userImage in userImages)
            {
                if (userImage.Id == request.Id && userImage.Profil==false)
                {
                    userImage.Profil = true;
                    userImage.ModifiedByName = request.ModifiedByName;
                    userImage.ModifiedTime = DateTime.Now;
                    await _unitOfWork.GetRepository<UserImage>().UpdateAsync(userImage);
                }
                if(userImage.Id != request.Id && userImage.Profil)
                {
                    userImage.Profil = false;
                    userImage.ModifiedByName = request.ModifiedByName;
                    userImage.ModifiedTime = DateTime.Now;
                    await _unitOfWork.GetRepository<UserImage>().UpdateAsync(userImage);
                    
                }
            }
            await _unitOfWork.SaveAsync();
            return new SetProfilImageCommandResponse
            {
                Result = new Result(ResultStatus.Success, Messages.UserImageSetProfil)
            };
        }
        return new SetProfilImageCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.UserImageNotFound)
        };
    }
}