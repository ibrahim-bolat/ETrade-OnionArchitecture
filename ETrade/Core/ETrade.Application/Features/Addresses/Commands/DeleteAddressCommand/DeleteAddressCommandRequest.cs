using ETrade.Application.Features.Addresses.DTOs;
using MediatR;

namespace ETrade.Application.Features.Addresses.Commands.DeleteAddressCommand;

public class DeleteAddressCommandRequest:IRequest<DeleteAddressCommandResponse>
{
    public int Id { get; set; }
    public string ModifiedByName { get; set; }
}