using MediatR;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetIpAdressesByAreaNameandMenuNameQuery;

public class GetIpAdressesByAreaNameandMenuNameQueryRequest:IRequest<GetIpAdressesByAreaNameandMenuNameQueryResponse>
{
    public string AreaName { get; set; }
    public string MenuName { get; set; }
}