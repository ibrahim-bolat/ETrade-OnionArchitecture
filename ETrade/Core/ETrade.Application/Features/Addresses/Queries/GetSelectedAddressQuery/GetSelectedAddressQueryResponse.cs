
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.Addresses.Queries.GetSelectedAddressQuery;

public class GetSelectedAddressQueryResponse
{
    public IDataResult<AddressDto> Result { get; set; }
}