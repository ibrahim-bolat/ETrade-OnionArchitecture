using AutoMapper;
using ETrade.Application.Constants;
using ETrade.Application.Features.Accounts.DTOs;
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Application.Features.Accounts.Queries.GetByIdForUserSummaryCardQuery;

public class GetByIdForUserSummaryCardQueryHandler:IRequestHandler<GetByIdForUserSummaryCardQueryRequest,GetByIdForUserSummaryCardQueryResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public GetByIdForUserSummaryCardQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<GetByIdForUserSummaryCardQueryResponse> Handle(GetByIdForUserSummaryCardQueryRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.Include(x=>x.Addresses.Where(a=>a.IsActive)).FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive);
        if (user != null)
        {
            if (user.IsActive)
            {
                UserSummaryCardDto userCardSummaryDto = _mapper.Map<UserSummaryCardDto>(user);
                return new GetByIdForUserSummaryCardQueryResponse{
                    Result = new DataResult<UserSummaryCardDto>(ResultStatus.Success, userCardSummaryDto)
                };
            }
            return new GetByIdForUserSummaryCardQueryResponse{
                Result = new DataResult<UserSummaryCardDto>(ResultStatus.Error, Messages.UserNotActive,null)
            };
        }
        return new GetByIdForUserSummaryCardQueryResponse{
            Result = new DataResult<UserSummaryCardDto>(ResultStatus.Error, Messages.UserNotFound,null)
        };
    }
}