using MediatR;

namespace ETrade.Application.Features.RoleOperations.Commands.SaveAuthorizeDefinitionEndpointsCommand;

public class SaveAuthorizeDefinitionEndpointsCommandRequest:IRequest<SaveAuthorizeDefinitionEndpointsCommandResponse>
{
    public List<int> CheckedIds{ get; set; }
}