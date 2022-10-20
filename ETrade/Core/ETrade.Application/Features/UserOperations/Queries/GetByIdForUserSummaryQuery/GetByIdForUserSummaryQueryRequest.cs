using MediatR;

namespace ETrade.Application.Features.UserOperations.Queries.GetByIdForUserSummaryQuery;

public class GetByIdForUserSummaryQueryRequest:IRequest<GetByIdForUserSummaryQueryResponse>
{
    public string Id { get; set; }
}