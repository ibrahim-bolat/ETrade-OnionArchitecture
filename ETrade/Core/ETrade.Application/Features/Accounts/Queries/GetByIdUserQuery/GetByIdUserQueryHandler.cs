using AutoMapper;
using ETrade.Application.Features.Accounts.Constants;
using ETrade.Application.Features.Accounts.DTOs.UserDtos;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.Accounts.Queries.GetByIdUserQuery;

public class GetByIdUserQueryHandler:IRequestHandler<GetByIdUserQueryRequest,GetByIdUserQueryResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public GetByIdUserQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<GetByIdUserQueryResponse> Handle(GetByIdUserQueryRequest request, CancellationToken cancellationToken)
    {
        AppUser user = await _userManager.FindByIdAsync(request.Id.ToString());
        if (user != null)
        {
            if (user.IsActive)
            {
                UserDto userDto = _mapper.Map<UserDto>(user);
                return new GetByIdUserQueryResponse
                {
                    Result = new DataResult<UserDto>(ResultStatus.Success,userDto)
                };
            }
            return new GetByIdUserQueryResponse{
                Result = new DataResult<UserDto>(ResultStatus.Error, Messages.UserNotActive,null)
            };
        }
        return new GetByIdUserQueryResponse{
            Result = new DataResult<UserDto>(ResultStatus.Error, Messages.UserNotFound,null)
        };
    }
}