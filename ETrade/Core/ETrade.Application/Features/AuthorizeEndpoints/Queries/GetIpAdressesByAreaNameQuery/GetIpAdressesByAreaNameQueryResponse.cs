using ETrade.Application.Features.AuthorizeEndpoints.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetIpAdressesByAreaNameQuery;

public class GetIpAdressesByAreaNameQueryResponse
{
    public IDataResult<List<IpAssignDto>> Result { get; set; }
}