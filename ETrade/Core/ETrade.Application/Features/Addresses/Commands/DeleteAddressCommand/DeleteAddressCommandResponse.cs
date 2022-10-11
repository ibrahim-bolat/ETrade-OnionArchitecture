
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Wrappers.Abstract;

namespace ETrade.Application.Features.Addresses.Commands.DeleteAddressCommand;

public class DeleteAddressCommandResponse
{
    public IDataResult<AddressDto> Result { get; set; }
}