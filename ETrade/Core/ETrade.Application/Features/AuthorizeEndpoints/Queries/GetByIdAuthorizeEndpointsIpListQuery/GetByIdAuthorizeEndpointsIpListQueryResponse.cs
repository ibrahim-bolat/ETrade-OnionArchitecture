using ETrade.Application.Features.AuthorizeEndpoints.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetByIdAuthorizeEndpointsIpListQuery;

public class GetByIdAuthorizeEndpointsIpListQueryResponse
{
    public IDataResult<List<IpAssignDto>> Result { get; set; }
}