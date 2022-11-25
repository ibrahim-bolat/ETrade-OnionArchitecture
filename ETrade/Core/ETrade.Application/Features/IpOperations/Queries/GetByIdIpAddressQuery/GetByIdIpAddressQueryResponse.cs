using ETrade.Application.Features.IpOperations.DTOs;
using ETrade.Application.Features.RoleOperations.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.IpOperations.Queries.GetByIdIpAddressQuery;

public class GetByIdIpAddressQueryResponse
{
    public IDataResult<IpDto> Result { get; set; }
}