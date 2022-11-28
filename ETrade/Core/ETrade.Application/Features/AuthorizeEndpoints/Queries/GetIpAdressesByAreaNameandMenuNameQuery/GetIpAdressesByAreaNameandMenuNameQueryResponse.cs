using ETrade.Application.Features.AuthorizeEndpoints.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetIpAdressesByAreaNameandMenuNameQuery;

public class GetIpAdressesByAreaNameandMenuNameQueryResponse
{
    public IDataResult<List<IpAssignDto>> Result { get; set; }
}