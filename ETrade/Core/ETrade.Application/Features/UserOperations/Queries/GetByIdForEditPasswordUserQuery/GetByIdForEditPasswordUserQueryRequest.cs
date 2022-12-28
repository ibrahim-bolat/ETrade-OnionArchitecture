using MediatR;

namespace ETrade.Application.Features.UserOperations.Queries.GetByIdForEditPasswordUserQuery;

public class GetByIdForEditPasswordUserQueryRequest:IRequest<GetByIdForEditPasswordUserQueryResponse>
{
    public string Id { get; set; }
}