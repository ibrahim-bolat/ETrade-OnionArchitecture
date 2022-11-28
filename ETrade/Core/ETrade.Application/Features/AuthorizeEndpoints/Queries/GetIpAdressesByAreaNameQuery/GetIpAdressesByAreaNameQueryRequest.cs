using MediatR;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetIpAdressesByAreaNameQuery;

public class GetIpAdressesByAreaNameQueryRequest:IRequest<GetIpAdressesByAreaNameQueryResponse>
{
    public string AreaName { get; set; }
}