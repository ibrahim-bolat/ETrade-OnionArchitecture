using ETrade.Application.DTOs.Common;
using MediatR;

namespace ETrade.Application.Features.RoleOperations.Queries.GetAuthorizeDefinitionEndpointsQuery;

public class GetAuthorizeDefinitionEndpointsQueryRequest:IRequest<GetAuthorizeDefinitionEndpointsQueryResponse>
{
    public Type Type { get; set; }
}