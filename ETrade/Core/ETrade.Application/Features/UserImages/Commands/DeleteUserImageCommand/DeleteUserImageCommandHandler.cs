using AutoMapper;
using ETrade.Application.Features.UserImages.Constants;
using ETrade.Application.Features.UserImages.DTOs;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace ETrade.Application.Features.UserImages.Commands.DeleteUserImageCommand;

public class DeleteUserImageCommandHandler:IRequestHandler<DeleteUserImageCommandRequest,DeleteUserImageCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _hostEnvironment;

    public DeleteUserImageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _hostEnvironment = hostEnvironment;
    }
    
    public async Task<DeleteUserImageCommandResponse> Handle(DeleteUserImageCommandRequest request, CancellationToken cancellationToken)
    {
        var userImage = await _unitOfWork.UserImageRepository.GetAsync(x => x.Id == request.Id && x.IsActive==true);
        if (userImage != null)
        {
            var imagePath = _hostEnvironment.WebRootPath + userImage.ImagePath;
            var userImagePath = $"{_hostEnvironment.WebRootPath}/admin/images/userimages/{userImage.UserId}";
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
                if(Directory.Exists(userImagePath) && Directory.GetFiles(userImagePath).Length==0)
                    Directory.Delete(userImagePath);
                userImage.IsActive = false;
                userImage.IsDeleted = true;
                userImage.ModifiedByName = request.ModifiedByName;
                userImage.ModifiedTime = DateTime.Now;
                await _unitOfWork.UserImageRepository.UpdateAsync(userImage);
                var result = await _unitOfWork.SaveAsync();
                var userImageDto = _mapper.Map<UserImageDto>(userImage);
                if (result > 0)
                {
                    return new DeleteUserImageCommandResponse
                    {
                        Result = new DataResult<UserImageDto>(ResultStatus.Success, userImageDto)
                    };
                }
                return new DeleteUserImageCommandResponse
                {
                    Result = new DataResult<UserImageDto>(ResultStatus.Error, Messages.UserImageNotDeleted, null)
                };
            }
        }
        return new DeleteUserImageCommandResponse
        {
            Result = new DataResult<UserImageDto>(ResultStatus.Error, Messages.UserImageNotFound, null)
        };
    }

}