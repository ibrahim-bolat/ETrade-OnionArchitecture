using ETrade.Application.Features.AuthorizeEndpoints.Queries.GetAuthorizeEndpointsforAssignRoleQuery;
using MediatR;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetAuthorizeEndpointsforAssignIpQuery;

public class GetAuthorizeEndpointsforAssignIpQueryRequest:IRequest<GetAuthorizeEndpointsforAssignIpQueryResponse>
{
    public string Query { get; set; }
}