using MediatR;

namespace ETrade.Application.Features.Accounts.Queries.GetByIdForEditPasswordUserQuery;

public class GetByIdForEditPasswordUserQueryRequest:IRequest<GetByIdForEditPasswordUserQueryResponse>
{
    public string Id { get; set; }
}