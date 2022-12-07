
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.Addresses.Queries.GetCreateAddressDtoQuery;

public class GetCreateAddressDtoQueryResponse
{
    public IDataResult<CreateAddressDto> Result { get; set; }
}