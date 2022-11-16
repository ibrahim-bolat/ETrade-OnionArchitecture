using AutoMapper;
using ETrade.Application.Features.UserImages.Constants;
using ETrade.Application.Features.UserImages.DTOs;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;

namespace ETrade.Application.Features.UserImages.Commands.CreateUserImageCommand;

public class CreateUserImageCommandHandler:IRequestHandler<CreateUserImageCommandRequest,CreateUserImageCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUserImageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateUserImageCommandResponse> Handle(CreateUserImageCommandRequest request, CancellationToken cancellationToken)
    {
        var count = await _unitOfWork.GetRepository<UserImage>().CountAsync(x => x.UserId == request.UserImageAddDto.UserId && x.IsActive);
        if (count >= 4)
        {
            return new CreateUserImageCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.UserImageCountMoreThan4)
            };
        }
        UserImage userImage = _mapper.Map<UserImage>(request.UserImageAddDto);
        userImage.ImagePath = await UploadImage(request.UserImageAddDto); //Resmi kaydet wwwroot/admin/images/userimages/
        userImage.CreatedByName = request.CreatedByName;
        userImage.ModifiedByName = request.CreatedByName;
        userImage.CreatedTime=DateTime.Now;
        userImage.ModifiedTime=DateTime.Now;
        userImage.IsActive = true;
        userImage.IsDeleted = false;
        await _unitOfWork.GetRepository<UserImage>().AddAsync(userImage);
        if (count > 0  && userImage.Profil)
        {
            var userImages =
                await _unitOfWork.GetRepository<UserImage>().GetAllAsync(ui =>
                    ui.UserId == request.UserImageAddDto.UserId && ui.IsActive);
            if (userImages != null)
            {
                foreach (var uImage in userImages)
                {
                    if (uImage.Profil)
                    {
                        uImage.Profil = false;
                        uImage.ModifiedByName = request.CreatedByName;
                        uImage.ModifiedTime = DateTime.Now;
                        await _unitOfWork.GetRepository<UserImage>().UpdateAsync(uImage);
                    }
                }
            }
        }
        int result = await _unitOfWork.SaveAsync();
        if (result > 0)
        {
            return new CreateUserImageCommandResponse
            {
                Result = new DataResult<UserImageAddDto>(ResultStatus.Success, Messages.UserImageAdded, request.UserImageAddDto)
            };
        }
        return new CreateUserImageCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.UserImageNotAdded)
        };
    }
    private async Task<string> UploadImage(UserImageAddDto userImageAddDto)
    {
        var imageFileName = Path.GetFileNameWithoutExtension(userImageAddDto.ImageFile.FileName);

        var localImageFileDir = $"wwwroot/admin/images/userimages/{userImageAddDto.UserId}";// wwwroot/admin/images/userimages/1
        var extension = Path.GetExtension(userImageAddDto.ImageFile.FileName).ToLower();
        
        //local userimages/userId klasörü yoksa oluştur.
        if (!Directory.Exists(Path.Combine(localImageFileDir)))
        {
            Directory.CreateDirectory(Path.Combine(localImageFileDir));
        }
        
        var localImageFilePath = $"{localImageFileDir}/{imageFileName}{extension}"; // wwwroot/admin/images/userimages/1/profil.jpg

        int count = 1;
        var tempFileName = imageFileName;
        while (File.Exists(localImageFilePath))
        {
            imageFileName = string.Format("{0}{1}", tempFileName, count++);
            localImageFilePath = $"{localImageFileDir}/{imageFileName}{extension}";
        }

        var path = $"/admin/images/userimages/{userImageAddDto.UserId}/{imageFileName}{extension}";// /admin/images/userimages/1/profil.jpg

        using (Stream fileStream = new FileStream(localImageFilePath, FileMode.Create))
        {
            await userImageAddDto.ImageFile.CopyToAsync(fileStream);
        }
        return path;
    }
}