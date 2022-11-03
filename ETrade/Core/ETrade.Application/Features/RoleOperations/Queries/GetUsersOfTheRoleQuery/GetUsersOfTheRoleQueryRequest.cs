using ETrade.Application.DTOs.Common;
using MediatR;

namespace ETrade.Application.Features.RoleOperations.Queries.GetUsersOfTheRoleQuery;

public class GetUsersOfTheRoleQueryRequest:IRequest<GetUsersOfTheRoleQueryResponse>
{
    public string Id { get; set; }
    public DatatableRequestDto DatatableRequestDto { get; set; }
}