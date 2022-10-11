using AutoMapper;
using ETrade.Application.Features.UserImages.Constants;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Enums;
using MediatR;

namespace ETrade.Application.Features.UserImages.Commands.SetProfilImageCommand;

public class SetProfilImageCommandHandler:IRequestHandler<SetProfilImageCommandRequest,SetProfilImageCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SetProfilImageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SetProfilImageCommandResponse> Handle(SetProfilImageCommandRequest request, CancellationToken cancellationToken)
    {
        var userImages = await _unitOfWork.UserImageRepository.GetAllAsync(ui=>ui.UserId==request.UserId && ui.IsActive);
        if (userImages !=null)
        {
            foreach (var userImage in userImages)
            {
                if (userImage.Id != request.Id && userImage.Profil )
                {
                    userImage.Profil = false;
                    userImage.ModifiedByName = request.ModifiedByName;
                    userImage.ModifiedTime = DateTime.Now;
                    await _unitOfWork.UserImageRepository.UpdateAsync(userImage);
                }
                else
                {
                    if (userImage.Id == request.Id && userImage.Profil==false )
                    {
                        userImage.Profil = true;
                        userImage.ModifiedByName = request.ModifiedByName;
                        userImage.ModifiedTime = DateTime.Now;
                        await _unitOfWork.UserImageRepository.UpdateAsync(userImage);
                    }
                }
            }
            var result = await _unitOfWork.SaveAsync();
            if (result > 0)
            {
                return new SetProfilImageCommandResponse{
                    Result = new Result(ResultStatus.Success, Messages.UserImageSetProfil)
                };
            }
        }
        return new SetProfilImageCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.UserImageNotFound)
        };
    }
}