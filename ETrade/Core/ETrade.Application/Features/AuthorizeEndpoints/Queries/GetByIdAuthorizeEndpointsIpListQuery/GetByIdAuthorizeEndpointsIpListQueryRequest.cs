using MediatR;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetByIdAuthorizeEndpointsIpListQuery;

public class GetByIdAuthorizeEndpointsIpListQueryRequest:IRequest<GetByIdAuthorizeEndpointsIpListQueryResponse>
{
    public int Id { get; set; }
}