using MediatR;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetAuthorizeEndpointsQuery;

public class GetAuthorizeEndpointsQueryRequest:IRequest<GetAuthorizeEndpointsQueryResponse>
{
    public string Query { get; set; }
}