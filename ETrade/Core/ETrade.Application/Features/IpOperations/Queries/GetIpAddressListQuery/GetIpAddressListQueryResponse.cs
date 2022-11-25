using ETrade.Application.DTOs.Common;
using ETrade.Application.Features.IpOperations.DTOs;
using ETrade.Application.Features.RoleOperations.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.IpOperations.Queries.GetIpAddressListQuery;

public class GetIpAddressListQueryResponse
{
    public IDataResult<DatatableResponseDto<IpListDto>> Result { get; set; }
}