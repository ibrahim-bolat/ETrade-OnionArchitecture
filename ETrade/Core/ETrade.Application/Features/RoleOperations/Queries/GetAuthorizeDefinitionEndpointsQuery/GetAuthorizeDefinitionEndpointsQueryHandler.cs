using ETrade.Application.DTOs.Common;
using ETrade.Application.Model;
using ETrade.Application.Services;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Application.Constants;
using ETrade.Domain.Enums;
using MediatR;

namespace ETrade.Application.Features.RoleOperations.Queries.GetAuthorizeDefinitionEndpointsQuery;

public class GetAuthorizeDefinitionEndpointsQueryHandler:IRequestHandler<GetAuthorizeDefinitionEndpointsQueryRequest,GetAuthorizeDefinitionEndpointsQueryResponse>
{
    private readonly IAuthorizeDefinationService _authorizeDefinationService;

    public GetAuthorizeDefinitionEndpointsQueryHandler(IAuthorizeDefinationService authorizeDefinationService)
    {
        _authorizeDefinationService = authorizeDefinationService;
    }

    public Task<GetAuthorizeDefinitionEndpointsQueryResponse> Handle(GetAuthorizeDefinitionEndpointsQueryRequest request, CancellationToken cancellationToken)
    {
        var endpoints = _authorizeDefinationService.GetAuthorizeDefinitionEndpoints(request.Type);
        if (endpoints != null)
        {
            return Task.FromResult( new GetAuthorizeDefinitionEndpointsQueryResponse{
                Result = new DataResult<List<Menu>>(ResultStatus.Success, endpoints)
            });
        }
        return Task.FromResult(new GetAuthorizeDefinitionEndpointsQueryResponse{
            Result = new DataResult<List<Menu>>(ResultStatus.Error, Messages.NotFoundAuthorizeDefinitionEndpoints,null)
        });
    }
}