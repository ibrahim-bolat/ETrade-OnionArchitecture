using ETrade.Application.Wrappers.Concrete;
using ETrade.Application.Constants;
using ETrade.Application.DTOs.Common;
using ETrade.Application.Repositories;
using ETrade.Domain.Enums;
using MediatR;
using Action = ETrade.Domain.Entities.Action;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetAuthorizeEndpointsQuery;

public class
    GetAuthorizeEndpointsQueryHandler : IRequestHandler<GetAuthorizeEndpointsQueryRequest,
        GetAuthorizeEndpointsQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAuthorizeEndpointsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAuthorizeEndpointsQueryResponse> Handle(
        GetAuthorizeEndpointsQueryRequest request, CancellationToken cancellationToken)
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
                            id = menu.Id,
                            text = menu.ControllerName,
                            children = GetActions(menu.Actions)
                        }
                    );
                }
                else
                {
                    treeViewDtos.Add(
                        new TreeViewDto()
                        {
                            id = menu.Id,
                            text = menu.ControllerName,
                            children = null
                        });
                }
            }

            return new GetAuthorizeEndpointsQueryResponse
            {
                Result = new DataResult<List<TreeViewDto>>(ResultStatus.Success, treeViewDtos)
            };
        }

        return new GetAuthorizeEndpointsQueryResponse
        {
            Result = new DataResult<List<TreeViewDto>>(ResultStatus.Error,
                Messages.NotFoundAuthorizeEndpoints, null)
        };
    }

    private List<TreeViewDto> GetActions(List<Action> actions)
    {
        List<TreeViewDto> treeViewDtos = new List<TreeViewDto>();
        foreach (var action in actions)
        {
            treeViewDtos.Add(new TreeViewDto()
            {
                id = action.Id,
                text = $"<a class='btn btn-info mr-2' onclick='return getRole({action.Id.ToString()})'>" +
                       $"<i class='fa fa-tasks'>Rol Ata</i></a> {action.Definition}",
                children = null
            });
        }

        return treeViewDtos;
    }
}