using AutoMapper;
using ETrade.Application.Features.UserImages.Constants;
using ETrade.Application.Features.UserImages.DTOs;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;

namespace ETrade.Application.Features.UserImages.Queries.GetByUserIdAllUserImageQuery;

public class GetByUserIdAllUserImageQueryHandler:IRequestHandler<GetByUserIdAllUserImageQueryRequest,GetByUserIdAllUserImageQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByUserIdAllUserImageQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetByUserIdAllUserImageQueryResponse> Handle(GetByUserIdAllUserImageQueryRequest request, CancellationToken cancellationToken)
    {
        var userImages = await _unitOfWork.GetRepository<UserImage>().GetAllAsync(ui=>ui.UserId==request.UserId && ui.IsActive);
        var userImagesViewDtoList = _mapper.Map<IList<UserImageDto>>(userImages);
        if (userImages.Count > -1)
        {
            return new GetByUserIdAllUserImageQueryResponse{
                Result = new DataResult<IList<UserImageDto>>(ResultStatus.Success,userImagesViewDtoList)
            };
        }
        return new GetByUserIdAllUserImageQueryResponse{
            Result = new DataResult<IList<UserImageDto>>(ResultStatus.Error, Messages.UserImageNotFound,null)
        };
    }
}