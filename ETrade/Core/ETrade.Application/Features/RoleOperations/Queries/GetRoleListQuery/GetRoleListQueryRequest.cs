using ETrade.Application.DTOs.Common;
using MediatR;

namespace ETrade.Application.Features.RoleOperations.Queries.GetRoleListQuery;

public class GetRoleListQueryRequest:IRequest<GetRoleListQueryResponse>
{
    public DatatableRequestDto DatatableRequestDto { get; set; }
}