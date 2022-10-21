using AutoMapper;
using ETrade.Application.Constants;
using ETrade.Application.Features.Accounts.DTOs;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Application.Features.Accounts.Queries.GetByIdForDetailProfileUserQuery;

public class GetByIdForDetailProfileUserQueryHandler:IRequestHandler<GetByIdForDetailProfileUserQueryRequest,GetByIdForDetailProfileUserQueryResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public GetByIdForDetailProfileUserQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<GetByIdForDetailProfileUserQueryResponse> Handle(GetByIdForDetailProfileUserQueryRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.Include(x=>x.Addresses).FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive);
        if (user != null)
        {
            if (user.IsActive)
            {
                UserDto userDto = _mapper.Map<UserDto>(user);
                List<AddressSummaryDto> addressSummaryDtos =
                    _mapper.Map<List<AddressSummaryDto>>(user.Addresses.Where(a => a.IsActive));
                UserDetailDto userDetailDto = new UserDetailDto()
                {
                    UserDto = userDto,
                    UserAddressSummaryDtos = addressSummaryDtos
                };
                return new GetByIdForDetailProfileUserQueryResponse{
                    Result = new DataResult<UserDetailDto>(ResultStatus.Success, userDetailDto)
                };
            }
            return new GetByIdForDetailProfileUserQueryResponse{
                Result = new DataResult<UserDetailDto>(ResultStatus.Error, Messages.UserNotActive,null)
            };
        }
        return new GetByIdForDetailProfileUserQueryResponse{
            Result = new DataResult<UserDetailDto>(ResultStatus.Error, Messages.UserNotFound,null)
        };
    }
}