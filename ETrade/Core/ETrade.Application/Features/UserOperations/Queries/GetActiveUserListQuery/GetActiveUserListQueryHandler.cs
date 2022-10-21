using AutoMapper;
using ETrade.Application.DTOs.Common;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.UserOperations.Queries.GetActiveUserListQuery;

public class GetActiveUserListQueryHandler:IRequestHandler<GetActiveUserListQueryRequest,GetActiveUserListQueryResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public GetActiveUserListQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public Task<GetActiveUserListQueryResponse> Handle(GetActiveUserListQueryRequest request, CancellationToken cancellationToken)
    {
        var userData = _userManager.Users.Where(u=>u.IsActive==true).AsQueryable();
        int pageSize = request.Length == -1 ? userData.Count() : request.Length;
        int skip = request.Start;
        if (!(string.IsNullOrEmpty(request.SortColumn) && string.IsNullOrEmpty(request.SortColumnDirection)))
        {
            userData = userData.OrderBy(s => request.SortColumn + " " + request.SortColumnDirection);
            Func<AppUser, string> orderingFunction = (c => request.SortColumn == "FirstName" ? c.FirstName :
                request.SortColumn  == "LastName" ? c.LastName :
                request.SortColumn  == "UserName" ? c.UserName :
                request.SortColumn  == "Email" ? c.Email : c.FirstName);

            if (request.SortColumnDirection == "desc")
            {
                userData = userData.OrderByDescending(orderingFunction).AsQueryable();
            }
            else
            {
                userData = userData.OrderBy(orderingFunction).AsQueryable();
            }
        }

        if (!string.IsNullOrEmpty(request.SearchValue))
        {
            userData = userData.Where(m => m.FirstName.ToLower().Contains(request.SearchValue.ToLower())
                                           || m.LastName.ToLower().Contains(request.SearchValue.ToLower())
                                           || m.UserName.ToLower().Contains(request.SearchValue.ToLower())
                                           || m.Email.ToLower().Contains(request.SearchValue.ToLower()));
        }
        int recordsTotal = userData.Count();
        var data = userData.Skip(skip).Take(pageSize).ToList();
        List<UserSummaryDto> userSummaryDtos = _mapper.Map<List<UserSummaryDto>>(data);
        UserListDto userListDto = new UserListDto
        {
            Draw = request.Draw,
            RecordsFiltered = recordsTotal,
            RecordsTotal = recordsTotal,
            UserSummaryDtos = userSummaryDtos,
            IsSuccess = true
        };
        return Task.FromResult(new GetActiveUserListQueryResponse{
            Result = new DataResult<UserListDto>(ResultStatus.Success, userListDto)
        });
    }
}