using ETrade.Application.Features.AuthorizeEndpoints.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetIpAdressesByEndpointQuery;

public class GetIpAdressesByEndpointQueryResponse
{
    public IDataResult<HashSet<IpAssignDto>> Result { get; set; }
}