using AutoMapper;
using ETrade.Application.Extensions;
using ETrade.Application.Features.AuthorizeEndpoints.DTOs;
using ETrade.Application.Features.IpOperations.Constants;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetByIdAuthorizeEndpointsIpListQuery;

public class GetByIdAuthorizeEndpointsIpListQueryHandler:IRequestHandler<GetByIdAuthorizeEndpointsIpListQueryRequest,GetByIdAuthorizeEndpointsIpListQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdAuthorizeEndpointsIpListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetByIdAuthorizeEndpointsIpListQueryResponse> Handle(GetByIdAuthorizeEndpointsIpListQueryRequest request, CancellationToken cancellationToken)
    {
        Endpoint endpoint = await _unitOfWork.GetRepository<Endpoint>().GetAsync(a=>a.Id==request.Id,a=>a.IpAddresses);
        if (endpoint != null)
        {
            if (endpoint.IsActive)
            {
                List<IpAddress> allActiveIpAddresses =
                    await _unitOfWork.GetRepository<IpAddress>().GetAllAsync(ip => ip.IsActive);
                List<IpAddress> endpointIpAddress = endpoint.IpAddresses;
                List<IpAssignDto> assignIpAddress = new List<IpAssignDto>();
                allActiveIpAddresses.ForEach(ip => assignIpAddress.Add(new IpAssignDto
                {
                    Id = ip.Id,
                    RangeStart = ip.RangeStart,
                    RangeEnd = ip.RangeEnd,
                    IpListType = ip.IpListType.GetEnumDescription(),
                    HasAssign = endpointIpAddress != null && endpointIpAddress.Any(e=>e.Id==ip.Id)
                }));
                return new GetByIdAuthorizeEndpointsIpListQueryResponse{
                    Result = new DataResult<List<IpAssignDto>>(ResultStatus.Success, assignIpAddress)
                };
            }
            return new GetByIdAuthorizeEndpointsIpListQueryResponse{
                Result = new DataResult<List<IpAssignDto>>(ResultStatus.Error, Messages.IpNotActive,null)
            };
        }
        return new GetByIdAuthorizeEndpointsIpListQueryResponse{
            Result = new DataResult<List<IpAssignDto>>(ResultStatus.Error, Messages.IpNotFound,null)
        };
    }
}