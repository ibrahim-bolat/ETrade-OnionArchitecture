using MediatR;

namespace ETrade.Application.Features.Accounts.Queries.GetByIdForDetailProfileUserQuery;

public class GetByIdForDetailProfileUserQueryRequest:IRequest<GetByIdForDetailProfileUserQueryResponse>
{
    public int Id { get; set; }
}