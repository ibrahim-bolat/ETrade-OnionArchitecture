using ETrade.Application.Services;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Application.Constants;
using ETrade.Application.DTOs.Common;
using ETrade.Application.Model;
using ETrade.Application.Repositories;
using ETrade.Domain.Enums;
using MediatR;
using Action = ETrade.Domain.Entities.Action;

namespace ETrade.Application.Features.RoleOperations.Queries.GetAuthorizeDefinitionEndpointsQuery;

public class GetAuthorizeDefinitionEndpointsQueryHandler:IRequestHandler<GetAuthorizeDefinitionEndpointsQueryRequest,GetAuthorizeDefinitionEndpointsQueryResponse>
{
    private readonly IAuthorizeDefinationService _authorizeDefinationService;
    private readonly IUnitOfWork _unitOfWork;

    public GetAuthorizeDefinitionEndpointsQueryHandler(IAuthorizeDefinationService authorizeDefinationService, IUnitOfWork unitOfWork)
    {
        _authorizeDefinationService = authorizeDefinationService;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAuthorizeDefinitionEndpointsQueryResponse> Handle(
        GetAuthorizeDefinitionEndpointsQueryRequest request, CancellationToken cancellationToken)
    {
        var menus = await _unitOfWork.MenuRepository.GetAllAsync(m => m.IsActive, m => m.Actions);
        if (menus != null)
        {
            List<TreeViewDto> treeViewDtos = new();
            foreach (var menu in menus)
            {
                if (menu.Actions != null)
                {
                    treeViewDtos.Add(
                        new TreeViewDto()
                        {
                            id = "0"+ menu.Id.ToString(),
                            text = menu.Name,
                            @checked = menu.Checked,
                            children = GetActions(menu.Actions)
                        }
                    );
                }
                else
                {
                    treeViewDtos.Add(
                        new TreeViewDto()
                        {
                            id = menu.Id.ToString(),
                            text = menu.Name,
                            @checked = true,
                            children = null
                        });
                }
            }
            return new GetAuthorizeDefinitionEndpointsQueryResponse
            {
                Result = new DataResult<List<TreeViewDto>>(ResultStatus.Success, treeViewDtos)
            };
        }
        return new GetAuthorizeDefinitionEndpointsQueryResponse
        {
            Result = new DataResult<List<TreeViewDto>>(ResultStatus.Error,
                Messages.NotFoundAuthorizeDefinitionEndpoints, null)
        };
    }
    private List<TreeViewDto> GetActions(List<Action> actions)
    {
        List<TreeViewDto> treeViewDtos = new List<TreeViewDto>();
        foreach (var action in actions)
        {
            treeViewDtos.Add(new TreeViewDto()
            {
                id= action.Id.ToString(),
                text = action.Definition,
                @checked = action.Checked,
                children = null
            });
        }
        return treeViewDtos;
    }
}