using ETrade.Application.Wrappers.Concrete;
using ETrade.Application.Constants;
using ETrade.Application.DTOs.Common;
using ETrade.Application.Repositories;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;

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
        List<Endpoint> endpoints = await _unitOfWork.GetRepository<Endpoint>().GetAllAsync(e => e.IsActive);
        HashSet<string> menus = new();
        endpoints.ForEach(e => menus.Add(e.ControllerName));
        List<TreeViewDto> treeViewDtos = new();
        int menuCount = 0;
        if (endpoints != null && menus != null)
        {
            foreach (var menu in menus)
            {
                treeViewDtos.Add(
                    new TreeViewDto()
                    {
                        id = $"m{menuCount++}",
                        text = $"{menu} <span class='badge badge-pill badge-success'>{endpoints.Where(e => e.ControllerName == menu).ToList().Count}</span>" ,
                        children = GetEndpoints(endpoints.Where(e => e.ControllerName == menu).ToList()),
                    }
                );
            }
            menuCount = 0;
            return new GetAuthorizeEndpointsQueryResponse
            {
                Result = new DataResult<List<TreeViewDto>>(ResultStatus.Success, treeViewDtos)
            };
        }
        return new GetAuthorizeEndpointsQueryResponse
        {
            Result = new DataResult<List<TreeViewDto>>(ResultStatus.Error, Messages.NotFoundAuthorizeEndpoints, null,null)
        };

    }

    private List<TreeViewDto> GetEndpoints(List<Endpoint> endpoints)
    {
        List<TreeViewDto> treeViewDtos = new List<TreeViewDto>();
        foreach (var endpoint in endpoints)
        {
            treeViewDtos.Add(new TreeViewDto()
            {
                id = endpoint.Id.ToString(),
                text = $"<a class='btn btn-info mr-2' onclick='return getRole({endpoint.Id.ToString()})'>" +
                       $"<i class='fa fa-tasks'>Rol Ata</i></a> {endpoint.Definition}",
                children = null
            });
        }
        return treeViewDtos;
    }
}