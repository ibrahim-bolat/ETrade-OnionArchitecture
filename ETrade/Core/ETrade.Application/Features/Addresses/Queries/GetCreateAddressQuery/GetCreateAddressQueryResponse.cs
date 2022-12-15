
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.Addresses.Queries.GetCreateAddressQuery;

public class GetCreateAddressQueryResponse
{
    public IDataResult<AddressDto> Result { get; set; }
}