using MediatR;

namespace ETrade.Application.Features.UserOperations.Queries.GetActiveUserListQuery;

public class GetActiveUserListQueryRequest:IRequest<GetActiveUserListQueryResponse>
{
    public string Draw { get; set; }
    public int Start { get; set; }
    public int Length { get; set; }
    public string SortColumn { get; set; }
    public string SortColumnDirection { get; set; }
    public string SearchValue { get; set; }
}