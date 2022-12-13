using ETrade.Application.Features.Addresses.DTOs;
using MediatR;

namespace ETrade.Application.Features.Addresses.Commands.CreateAddressCommand;

public class CreateAddressCommandRequest:IRequest<CreateAddressCommandResponse>
{
    public AddressDto AddressDto { get; set; }
}