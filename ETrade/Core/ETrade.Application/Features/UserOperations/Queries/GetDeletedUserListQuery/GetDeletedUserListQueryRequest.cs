using ETrade.Application.DTOs.Common;
using MediatR;

namespace ETrade.Application.Features.UserOperations.Queries.GetDeletedUserListQuery;

public class GetDeletedUserListQueryRequest:IRequest<GetDeletedUserListQueryResponse>
{
    public DatatableRequestDto DatatableRequestDto { get; set; }
}