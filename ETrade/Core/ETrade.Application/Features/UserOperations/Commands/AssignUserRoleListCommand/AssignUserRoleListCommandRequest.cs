using MediatR;

namespace ETrade.Application.Features.UserOperations.Commands.AssignUserRoleListCommand;

public class AssignUserRoleListCommandRequest:IRequest<AssignUserRoleListCommandResponse>
{
    public string Id { get; set; }
    public List<int> RoleIds { get; set; }
}