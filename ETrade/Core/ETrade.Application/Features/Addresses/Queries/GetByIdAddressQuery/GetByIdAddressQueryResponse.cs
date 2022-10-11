
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.Addresses.Queries.GetByIdAddressQuery;

public class GetByIdAddressQueryResponse
{
    public IDataResult<AddressDto> Result { get; set; }
}