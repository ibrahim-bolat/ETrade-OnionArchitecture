using ETrade.Application.DTOs;
using ETrade.Application.DTOs.Common;
using ETrade.Application.Model;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetAuthorizeEndpointsforAssignIpQuery;

public class GetAuthorizeEndpointsforAssignIpQueryResponse
{
    public IDataResult<List<TreeViewDto>> Result { get; set; }
}