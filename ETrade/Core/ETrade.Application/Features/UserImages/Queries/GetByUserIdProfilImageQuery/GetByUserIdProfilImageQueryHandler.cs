using AutoMapper;
using ETrade.Application.Features.UserImages.Constants;
using ETrade.Application.Features.UserImages.DTOs;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;

namespace ETrade.Application.Features.UserImages.Queries.GetByUserIdProfilImageQuery;

public class GetByUserIdProfilImageQueryHandler:IRequestHandler<GetByUserIdProfilImageQueryRequest,GetByUserIdProfilImageQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByUserIdProfilImageQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetByUserIdProfilImageQueryResponse> Handle(GetByUserIdProfilImageQueryRequest request, CancellationToken cancellationToken)
    {
        var userImage = await _unitOfWork.GetRepository<UserImage>().GetAsync(predicate:x => x.UserId == request.UserId && x.IsActive && x.Profil);
        var userImageViewDto = _mapper.Map<UserImageDto>(userImage);
        if (userImage != null)
        {
            return new GetByUserIdProfilImageQueryResponse{
                Result = new DataResult<UserImageDto>(ResultStatus.Success,userImageViewDto)
            };
        }
        return new GetByUserIdProfilImageQueryResponse{
            Result = new DataResult<UserImageDto>(ResultStatus.Error, Messages.UserImageNotFound,null)
        };
    }
}