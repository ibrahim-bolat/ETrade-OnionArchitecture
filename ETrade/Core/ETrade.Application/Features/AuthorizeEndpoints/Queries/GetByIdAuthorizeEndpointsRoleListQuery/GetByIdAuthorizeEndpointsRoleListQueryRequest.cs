using MediatR;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetByIdAuthorizeEndpointsRoleListQuery;

public class GetByIdAuthorizeEndpointsRoleListQueryRequest:IRequest<GetByIdAuthorizeEndpointsRoleListQueryResponse>
{
    public string Id { get; set; }
}