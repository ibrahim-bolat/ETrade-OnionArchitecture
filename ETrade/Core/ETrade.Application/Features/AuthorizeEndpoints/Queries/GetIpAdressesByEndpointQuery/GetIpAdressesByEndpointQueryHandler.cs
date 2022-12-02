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

public class GetIpAdressesByEndpointQueryHandler : IRequestHandler<GetIpAdressesByEndpointQueryRequest,
    GetIpAdressesByEndpointQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetIpAdressesByEndpointQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetIpAdressesByEndpointQueryResponse> Handle(GetIpAdressesByEndpointQueryRequest request,
        CancellationToken cancellationToken)
    {
        List<Endpoint> endpointList = null;
        if (request.AreaName != null && request.EndpointId == 0)
        {
            if (request.MenuName == null)
            {
                endpointList = await _unitOfWork.GetRepository<Endpoint>().GetAllAsync(a => 
                    a.AreaName == request.AreaName, a => a.IpAddresses);
            }
            else
            {
                endpointList = await _unitOfWork.GetRepository<Endpoint>().GetAllAsync(a => 
                    a.AreaName == request.AreaName && a.ControllerName == request.MenuName, a => a.IpAddresses);
            }
            if (endpointList != null)
            {
                List<IpAddress> allActiveIpAddresses =
                    await _unitOfWork.GetRepository<IpAddress>().GetAllAsync(ip => ip.IsActive);
                HashSet<IpAssignDto> assignIpAddress = new HashSet<IpAssignDto>();
                foreach (var activeIpAddress in allActiveIpAddresses)
                {
                    assignIpAddress.Add(new IpAssignDto
                    {
                        Id = activeIpAddress.Id,
                        RangeStart = activeIpAddress.RangeStart,
                        RangeEnd = activeIpAddress.RangeEnd,
                        IpListType = activeIpAddress.IpListType.GetEnumDescription(),
                        TobeAssignedAreaName = request.AreaName,
                        TobeAssignedMenuName = request.MenuName,
                        TobeAssignedEndpointId = null,
                        HasAssign = endpointList.All(e => e.IpAddresses.Any(i => i.Id == activeIpAddress.Id))
                    });
                }

                return new GetIpAdressesByEndpointQueryResponse
                {
                    Result = new DataResult<HashSet<IpAssignDto>>(ResultStatus.Success, assignIpAddress)
                };
            }

            return new GetIpAdressesByEndpointQueryResponse
            {
                Result = new DataResult<HashSet<IpAssignDto>>(ResultStatus.Error, Messages.IpNotFound, null)
            };
        }
        
        if (request.AreaName == null && request.MenuName == null && request.EndpointId > 0)
        {
            Endpoint endpoint = await _unitOfWork.GetRepository<Endpoint>()
                .GetAsync(a => a.Id == request.EndpointId, a => a.IpAddresses);
            if (endpoint != null)
            {
                List<IpAddress> allActiveIpAddresses =
                    await _unitOfWork.GetRepository<IpAddress>().GetAllAsync(ip => ip.IsActive);
                HashSet<IpAssignDto> assignIpAddress = new HashSet<IpAssignDto>();
                List<IpAddress> endpointIpAddress = endpoint.IpAddresses;
                foreach (var activeIpAddress in allActiveIpAddresses)
                {
                    assignIpAddress.Add(new IpAssignDto
                    {
                        Id = activeIpAddress.Id,
                        RangeStart = activeIpAddress.RangeStart,
                        RangeEnd = activeIpAddress.RangeEnd,
                        IpListType = activeIpAddress.IpListType.GetEnumDescription(),
                        TobeAssignedAreaName = null,
                        TobeAssignedMenuName = null,
                        TobeAssignedEndpointId = endpoint.Id.ToString(),
                        HasAssign = endpointIpAddress != null &&
                                    endpointIpAddress.Any(e => e.Id == activeIpAddress.Id)
                    });
                }

                return new GetIpAdressesByEndpointQueryResponse
                {
                    Result = new DataResult<HashSet<IpAssignDto>>(ResultStatus.Success, assignIpAddress)
                };
            }

            return new GetIpAdressesByEndpointQueryResponse
            {
                Result = new DataResult<HashSet<IpAssignDto>>(ResultStatus.Error, Messages.IpNotFound, null)
            };
        }
        return new GetIpAdressesByEndpointQueryResponse
        {
            Result = new DataResult<HashSet<IpAssignDto>>(ResultStatus.Error, AuthorizeEndpoints.Constants.Messages.EndpointNotFound, null)
        };
    }
}