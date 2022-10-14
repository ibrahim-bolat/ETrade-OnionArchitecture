using MediatR;

namespace ETrade.Application.Features.Accounts.Queries.GetByIdForUserSummaryCardQuery;

public class GetByIdForUserSummaryCardQueryRequest:IRequest<GetByIdForUserSummaryCardQueryResponse>
{
    public int Id { get; set; }
}