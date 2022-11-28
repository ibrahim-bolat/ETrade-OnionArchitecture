using MediatR;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetIpAdressesByEndpointQuery;

public class GetIpAdressesByEndpointQueryRequest:IRequest<GetIpAdressesByEndpointQueryResponse>
{
    public string AreaName { get; set; }
    public string MenuName { get; set; }
    public int Id { get; set; }
}