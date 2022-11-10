using ETrade.Application.DTOs;
using ETrade.Application.DTOs.Common;
using ETrade.Application.Model;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetAuthorizeEndpointsQuery;

public class GetAuthorizeEndpointsQueryResponse
{
    public IDataResult<List<TreeViewDto>> Result { get; set; }
}