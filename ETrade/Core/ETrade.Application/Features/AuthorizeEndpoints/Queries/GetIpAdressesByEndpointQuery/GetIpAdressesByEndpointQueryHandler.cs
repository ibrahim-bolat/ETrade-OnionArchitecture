using AutoMapper;
using ETrade.Application.Extensions;
using ETrade.Application.Features.AuthorizeEndpoints.DTOs;
using ETrade.Application.Features.IpOperations.Constants;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetIpAdressesByEndpointQuery;

public class GetIpAdressesByEndpointQueryHandler:IRequestHandler<GetIpAdressesByEndpointQueryRequest,GetIpAdressesByEndpointQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetIpAdressesByEndpointQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetIpAdressesByEndpointQueryResponse> Handle(GetIpAdressesByEndpointQueryRequest request, CancellationToken cancellationToken)
    {
        List<Endpoint> endpointList = null;
        if (request.AreaName != null && request.MenuName == null && request.Id==0)
        {
            endpointList = await _unitOfWork.GetRepository<Endpoint>().GetAllAsync(a=>a.AreaName==request.AreaName,a=>a.IpAddresses);
        }
        if (request.AreaName != null && request.MenuName != null && request.Id==0)
        {
            endpointList = await _unitOfWork.GetRepository<Endpoint>().GetAllAsync(a=>a.AreaName==request.AreaName && a.ControllerName==request.MenuName,a=>a.IpAddresses);
        }
        if (request.AreaName == null && request.MenuName == null && request.Id>0)
        { 
            endpointList = await _unitOfWork.GetRepository<Endpoint>().GetAllAsync(a=>a.Id==request.Id,a=>a.IpAddresses);
        }
        
        if (endpointList != null)
        {
            List<IpAddress> allActiveIpAddresses =
                await _unitOfWork.GetRepository<IpAddress>().GetAllAsync(ip => ip.IsActive);
            HashSet<IpAssignDto> assignIpAddress = new HashSet<IpAssignDto>();
            foreach (var endpoint in endpointList)
            {
                if (endpoint.IsActive)
                {
                    foreach (var activeIpAddress in allActiveIpAddresses)
                    {
                        List<IpAddress> endpointIpAddress = endpoint.IpAddresses;
                        if (!assignIpAddress.Any(ip => ip.Id == activeIpAddress.Id))
                        {
                            assignIpAddress.Add(new IpAssignDto
                            {
                                Id = activeIpAddress.Id,
                                RangeStart = activeIpAddress.RangeStart,
                                RangeEnd = activeIpAddress.RangeEnd,
                                IpListType = activeIpAddress.IpListType.GetEnumDescription(),
                                TobeAssignedAreaName = ((request.AreaName != null && request.MenuName == null && request.Id==0)||
                                                        (request.AreaName != null && request.MenuName != null && request.Id==0))? request.AreaName:string.Empty,
                                TobeAssignedMenuName = (request.AreaName != null && request.MenuName != null && request.Id==0)? request.MenuName:string.Empty,
                                TobeAssignedEndpointId = (request.AreaName == null && request.MenuName == null && request.Id>0) ? endpoint.Id.ToString():string.Empty,
                                HasAssign = endpointIpAddress != null &&
                                            endpointIpAddress.Any(e => e.Id == activeIpAddress.Id)
                            });
                        }
                    }
                }
            }
            return new GetIpAdressesByEndpointQueryResponse{
                Result = new DataResult<HashSet<IpAssignDto>>(ResultStatus.Success, assignIpAddress)
            };
        }
        return new GetIpAdressesByEndpointQueryResponse{
            Result = new DataResult<HashSet<IpAssignDto>>(ResultStatus.Error, Messages.IpNotFound,null)
        };
    }
}