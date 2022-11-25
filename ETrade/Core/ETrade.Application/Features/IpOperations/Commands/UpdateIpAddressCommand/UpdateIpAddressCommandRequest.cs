using ETrade.Application.Features.IpOperations.DTOs;
using ETrade.Application.Features.RoleOperations.DTOs;
using MediatR;

namespace ETrade.Application.Features.IpOperations.Commands.UpdateIpAddressCommand;

public class UpdateIpAddressCommandRequest:IRequest<UpdateIpAddressCommandResponse>
{
    public IpDto IpDto{ get; set; }
}