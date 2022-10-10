using ETrade.Application.DTOs.AddressDtos;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Services.Abstract;

public interface IAddressService
{
    Task<IResult> AddAsync(AddressDto addressDto, string createdByName);
    Task<IResult> UpdateAsync(AddressDto addressDto, string modifiedByName);
    Task<IDataResult<AddressDto>> DeleteAsync(int id, string modifiedByName);
    Task<IDataResult<AddressDto>> GetAsync(int id);
}