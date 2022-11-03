using AutoMapper;
using ETrade.Application.DTOs.Common;
using ETrade.Application.Features.UserOperations.DTOs;
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
        int pageSize = request.DatatableRequestDto.Length == -1 ? userData.Count() :  request.DatatableRequestDto.Length;
        int skip =  request.DatatableRequestDto.Start;
        var sortColumn = request.DatatableRequestDto.Columns[request.DatatableRequestDto.Order[0].Column].Data;
        var sortColumnDirection = request.DatatableRequestDto.Order[0].Dir.ToString();
        if (!string.IsNullOrEmpty(request.DatatableRequestDto.Search.Value))
        {
            userData = userData.Where(m => m.FirstName.ToLower().Contains(request.DatatableRequestDto.Search.Value.ToLower())
                                           || m.LastName.ToLower().Contains(request.DatatableRequestDto.Search.Value.ToLower())
                                           || m.UserName.ToLower().Contains(request.DatatableRequestDto.Search.Value.ToLower())
                                           || m.Email.ToLower().Contains(request.DatatableRequestDto.Search.Value.ToLower()));
        }
        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            Func<AppUser, string> orderingFunction = (c => sortColumn == nameof(c.FirstName) ? c.FirstName :
                sortColumn  == nameof(c.LastName) ? c.LastName :
                sortColumn  == nameof(c.UserName) ? c.UserName :
                sortColumn  == nameof(c.Email) ? c.Email : c.Id.ToString());

            if (sortColumnDirection == OrderDirType.Desc.ToString())
            {
                userData = userData.OrderByDescending(orderingFunction).AsQueryable();
            }
            else
            {
                userData = userData.OrderBy(orderingFunction).AsQueryable();
            }
        }
        int recordsTotal = userData.Count();
        var data = userData.Skip(skip).Take(pageSize).ToList();
        List<UserSummaryDto> activeUser = _mapper.Map<List<UserSummaryDto>>(data);
        var response = new DatatableResponseDto<UserSummaryDto>
        {
            Draw = request.DatatableRequestDto.Draw,
            RecordsTotal = recordsTotal,
            RecordsFiltered = recordsTotal,
            Data = activeUser
        };
        return Task.FromResult(new GetActiveUserListQueryResponse{
            Result = new DataResult<DatatableResponseDto<UserSummaryDto>>(ResultStatus.Success, response)
        });
    }
}