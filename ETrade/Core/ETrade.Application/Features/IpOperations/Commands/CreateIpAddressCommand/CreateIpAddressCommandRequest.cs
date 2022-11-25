using ETrade.Application.Features.IpOperations.DTOs;
using ETrade.Application.Features.RoleOperations.DTOs;
using MediatR;

namespace ETrade.Application.Features.IpOperations.Commands.CreateIpAddressCommand;

public class CreateIpAddressCommandRequest:IRequest<CreateIpAddressCommandResponse>
{
    public IpDto IpDto{ get; set; }
}