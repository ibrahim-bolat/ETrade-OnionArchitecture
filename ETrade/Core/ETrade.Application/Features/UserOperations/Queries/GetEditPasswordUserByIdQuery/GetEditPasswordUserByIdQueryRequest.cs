using MediatR;

namespace ETrade.Application.Features.UserOperations.Queries.GetEditPasswordUserByIdQuery;

public class GetEditPasswordUserByIdQueryRequest:IRequest<GetEditPasswordUserByIdQueryResponse>
{
    public string Id { get; set; }
}