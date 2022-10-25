using ETrade.Application.Features.UserOperations.DTOs;
using MediatR;

namespace ETrade.Application.Features.UserOperations.Queries.GetActiveUserListQuery;

public class GetActiveUserListQueryRequest:IRequest<GetActiveUserListQueryResponse>
{
    public DatatableRequestDto DatatableRequestDto { get; set; }
}