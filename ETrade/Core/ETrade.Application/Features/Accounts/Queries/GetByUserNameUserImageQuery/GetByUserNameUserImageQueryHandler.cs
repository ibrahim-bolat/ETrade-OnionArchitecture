using AutoMapper;
using ETrade.Application.Constants;
using ETrade.Application.Features.UserImages.DTOs;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Application.Features.Accounts.Queries.GetByUserNameUserImageQuery;

public class GetByUserNameUserImageQueryHandler:IRequestHandler<GetByUserNameUserImageQueryRequest,GetByUserNameUserImageQueryResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public GetByUserNameUserImageQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<GetByUserNameUserImageQueryResponse> Handle(GetByUserNameUserImageQueryRequest request, CancellationToken cancellationToken)
    {
        AppUser user = await _userManager.Users.Include(x => x.UserImages.Where(a => a.IsActive))
            .FirstOrDefaultAsync(x => x.UserName == request.UserName && x.IsActive);
        if (user != null)
        {
            if (user.IsActive)
            {
                List<UserImageDto> userImageDtos;
                if (user.UserImages.Count > 0)
                {
                   userImageDtos =
                        _mapper.Map<List<UserImageDto>>(user.UserImages.Where(a => a.IsActive));

                    return new GetByUserNameUserImageQueryResponse
                    {
                        Result = new DataResult<List<UserImageDto>>(ResultStatus.Success, userImageDtos)
                    };
                } ;
                userImageDtos = new List<UserImageDto>{ new UserImageDto
                    {
                        UserId = user.Id
                    }
                };
                return new GetByUserNameUserImageQueryResponse
                {
                    Result = new DataResult<List<UserImageDto>>(ResultStatus.Success, userImageDtos)
                };
            }
            return new GetByUserNameUserImageQueryResponse{
                Result = new DataResult<List<UserImageDto>>(ResultStatus.Error, Messages.UserNotActive,null)
            };
        }
        return new GetByUserNameUserImageQueryResponse{
            Result = new DataResult<List<UserImageDto>>(ResultStatus.Error, Messages.UserNotFound,null)
        };
    }
}