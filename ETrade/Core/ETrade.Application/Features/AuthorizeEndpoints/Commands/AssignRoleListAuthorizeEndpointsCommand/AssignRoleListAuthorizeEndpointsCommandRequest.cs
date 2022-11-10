using MediatR;

namespace ETrade.Application.Features.AuthorizeEndpoints.Commands.AssignRoleListAuthorizeEndpointsCommand;

public class AssignRoleListAuthorizeEndpointsCommandRequest:IRequest<AssignRoleListAuthorizeEndpointsCommandResponse>
{
    public int Id { get; set; }
    public List<int> RoleIds { get; set; }
}