
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.Addresses.Queries.GetByIdUpdateAddressQuery;

public class GetByIdUpdateAddressQueryResponse
{
    public IDataResult<AddressDto> Result { get; set; }
}