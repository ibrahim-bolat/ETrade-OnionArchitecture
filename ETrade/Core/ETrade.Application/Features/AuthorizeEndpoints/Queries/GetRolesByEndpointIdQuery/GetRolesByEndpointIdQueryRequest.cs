using MediatR;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetRolesByEndpointIdQuery;

public class GetRolesByEndpointIdQueryRequest:IRequest<GetRolesByEndpointIdQueryResponse>
{
    public string Id { get; set; }
}