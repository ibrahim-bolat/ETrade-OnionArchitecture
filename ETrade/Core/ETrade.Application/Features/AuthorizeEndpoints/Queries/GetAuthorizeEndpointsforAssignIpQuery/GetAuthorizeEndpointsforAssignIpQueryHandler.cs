using ETrade.Application.Wrappers.Concrete;
using ETrade.Application.Constants;
using ETrade.Application.DTOs.Common;
using ETrade.Application.Repositories;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;
using Newtonsoft.Json;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetAuthorizeEndpointsforAssignIpQuery;

public class
    GetAuthorizeEndpointsforAssignIpQueryHandler : IRequestHandler<GetAuthorizeEndpointsforAssignIpQueryRequest,
        GetAuthorizeEndpointsforAssignIpQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAuthorizeEndpointsforAssignIpQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAuthorizeEndpointsforAssignIpQueryResponse> Handle(
        GetAuthorizeEndpointsforAssignIpQueryRequest request, CancellationToken cancellationToken)
    {
        List<Endpoint> endpoints = await _unitOfWork.GetRepository<Endpoint>().GetAllAsync(e => e.IsActive);
        HashSet<string> areas = new();
        endpoints.ForEach(e=>areas.Add(e.AreaName));
        List<TreeViewDto> treeViewDtos = new();
        int areaCount = 1;
        if (endpoints != null && areas != null)
        {
            foreach (var area in areas)
            {
                string areaId = $"a{(areaCount++).ToString()}";
                treeViewDtos.Add(
                    new TreeViewDto()
                    {
                        id =  areaId,
                        text = $"<button type='submit' class='btn btn-primary mr-2 CustomEndpointClass IpAdressesByAreaName' id='getIpAdressesByAreaName-{areaId}' data-areaname ='{area}'>Ip Ata</button>" +
                               $"{area} <span id='areaEndpointCount' data-areaEndpointCount='{endpoints.Count(e => e.AreaName == area)}' class='badge badge-pill badge-primary' style = 'font-size: 16px;'>{endpoints.Count(e => e.AreaName == area)}</span>" ,
                        children = GetMenus(area,endpoints.Where(e => e.AreaName == area).ToList()),
                    }
                );
            }
            areaCount = 1;
            return new GetAuthorizeEndpointsforAssignIpQueryResponse
            {
                Result = new DataResult<List<TreeViewDto>>(ResultStatus.Success, treeViewDtos)
            };
        }
        return new GetAuthorizeEndpointsforAssignIpQueryResponse
        {
            Result = new DataResult<List<TreeViewDto>>(ResultStatus.Error, Messages.NotFoundAuthorizeEndpoints, null,null)
        };

    }
    private List<TreeViewDto> GetMenus(string areaName , List<Endpoint> endpoints)
    {
        HashSet<string> menus = new();
        endpoints.ForEach(e=>menus.Add(e.ControllerName));
        int menuCount = 1;
        List<TreeViewDto> treeViewDtos = new List<TreeViewDto>();
        if (endpoints != null && menus != null)
        {
            foreach (var menu in menus)
            {
                string menuId = $"m{(menuCount++).ToString()}";
                treeViewDtos.Add(new TreeViewDto()
                {
                    id = menuId,
                    text = $"<button type='submit' class='btn btn-success mr-2 CustomEndpointClass IpAdressesByAreaNameandMenuName' id='getIpAdressesByAreaNameandMenuName-{menuId}' data-areaname ='{areaName}' data-menuname='{menu}'>Ip Ata</button>" +
                           $"{menu} <span id='menuEndpointCount' data-menuEndpointCount='{endpoints.Count(e => e.ControllerName == menu)}' " +
                           $"class='badge badge-pill badge-success' style = 'font-size: 16px;'>{endpoints.Count(e => e.ControllerName == menu)}</span>" ,
                    children = GetEndpoints(endpoints.Where(e => e.ControllerName == menu).ToList()),
                });
            }
            menuCount = 1;
            return treeViewDtos;
        }
        return null;
    }

    private List<TreeViewDto> GetEndpoints(List<Endpoint> endpoints)
    {
        List<TreeViewDto> treeViewDtos = new List<TreeViewDto>();
        if (endpoints != null)
        {
            foreach (var endpoint in endpoints)
            {
                treeViewDtos.Add(new TreeViewDto()
                {
                    id = endpoint.Id.ToString(),
                    text = $"<button type='submit' class='btn btn-info mr-2 CustomEndpointClass IpAdressesByEndpointId' id='getIpAdressesByEndpointId{endpoint.Id.ToString()}' data-id='{endpoint.Id.ToString()}'>Ip Ata</button> {endpoint.Definition}" ,
                    children = null
                });
            }
            return treeViewDtos; 
        }
        return null;
    }
}