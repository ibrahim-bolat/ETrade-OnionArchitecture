using MediatR;

namespace ETrade.Application.Features.IpOperations.Commands.SetIpAddressActiveCommand;

public class SetIpAddressActiveCommandRequest:IRequest<SetIpAddressActiveCommandResponse>
{
    public int Id { get; set; }
}