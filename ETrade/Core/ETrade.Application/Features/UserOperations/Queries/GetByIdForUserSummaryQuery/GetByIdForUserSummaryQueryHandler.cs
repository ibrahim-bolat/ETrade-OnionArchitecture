using AutoMapper;
using ETrade.Application.Features.Accounts.Constants;
using ETrade.Application.Features.UserOperations.DTOs;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Application.Features.UserOperations.Queries.GetByIdForUserSummaryQuery;

public class GetByIdForUserSummaryQueryHandler:IRequestHandler<GetByIdForUserSummaryQueryRequest,GetByIdForUserSummaryQueryResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public GetByIdForUserSummaryQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<GetByIdForUserSummaryQueryResponse> Handle(GetByIdForUserSummaryQueryRequest request, CancellationToken cancellationToken)
    {
        var user = await  _userManager.FindByIdAsync(request.Id);
        if (user != null)
        {
            if (user.IsActive)
            {
                UserSummaryDto userSummaryDto = _mapper.Map<UserSummaryDto>(user);
                return new GetByIdForUserSummaryQueryResponse{
                    Result = new DataResult<UserSummaryDto>(ResultStatus.Success, userSummaryDto)
                };
            }
            return new GetByIdForUserSummaryQueryResponse{
                Result = new DataResult<UserSummaryDto>(ResultStatus.Error, Messages.UserNotActive,null)
            };
        }
        return new GetByIdForUserSummaryQueryResponse{
            Result = new DataResult<UserSummaryDto>(ResultStatus.Error, Messages.UserNotFound,null)
        };
    }
}