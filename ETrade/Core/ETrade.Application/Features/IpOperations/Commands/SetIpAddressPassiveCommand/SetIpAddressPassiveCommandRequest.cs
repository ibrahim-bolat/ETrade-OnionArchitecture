using MediatR;

namespace ETrade.Application.Features.IpOperations.Commands.SetIpAddressPassiveCommand;

public class SetIpAddressPassiveCommandRequest:IRequest<SetIpAddressPassiveCommandResponse>
{
    public int Id { get; set; }
}