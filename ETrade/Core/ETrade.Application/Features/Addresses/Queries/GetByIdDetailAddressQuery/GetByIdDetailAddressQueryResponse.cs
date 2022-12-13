
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.Addresses.Queries.GetByIdDetailAddressQuery;

public class GetByIdDetailAddressQueryResponse
{
    public IDataResult<DetailAddressDto> Result { get; set; }
}