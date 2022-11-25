using ETrade.Application.Features.AuthorizeEndpoints.Queries.GetAuthorizeEndpointsforAssignRoleQuery;
using MediatR;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetAuthorizeEndpointsforAssignRoleQuery;

public class GetAuthorizeEndpointsforAssignRoleQueryRequest:IRequest<GetAuthorizeEndpointsforAssignRoleQueryResponse>
{
    public string Query { get; set; }
}