using MediatR;

namespace ETrade.Application.Features.AuthorizeEndpoints.Commands.AssignIpListAuthorizeEndpointsCommand;

public class AssignIpListAuthorizeEndpointsCommandRequest:IRequest<AssignIpListAuthorizeEndpointsCommandResponse>
{
    public string IpAreaName { get; set; }
    public string IpMenuName { get; set; }
    public int EndpointId { get; set; }
    public List<int> IpIds { get; set; }
}