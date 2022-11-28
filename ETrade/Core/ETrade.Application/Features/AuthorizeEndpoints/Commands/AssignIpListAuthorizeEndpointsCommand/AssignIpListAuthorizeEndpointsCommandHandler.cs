using ETrade.Application.Features.IpOperations.Constants;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;
using Endpoint = ETrade.Domain.Entities.Endpoint;

namespace ETrade.Application.Features.AuthorizeEndpoints.Commands.AssignIpListAuthorizeEndpointsCommand;

public class AssignIpListAuthorizeEndpointsCommandHandler : IRequestHandler<
    AssignIpListAuthorizeEndpointsCommandRequest, AssignIpListAuthorizeEndpointsCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignIpListAuthorizeEndpointsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AssignIpListAuthorizeEndpointsCommandResponse> Handle(
        AssignIpListAuthorizeEndpointsCommandRequest request, CancellationToken cancellationToken)
    {
        List<Endpoint> endpointList = new List<Endpoint>();
        List<IpAddress> allActiveIpAddresses =
            await _unitOfWork.GetRepository<IpAddress>().GetAllAsync(ip => ip.IsActive);
        if (request.IpAreaName != null && request.IpMenuName == null)
        {
            endpointList = await _unitOfWork.GetRepository<Endpoint>()
                .GetAllAsync(e => e.AreaName == request.IpAreaName, a => a.IpAddresses);
            foreach (var endpoint in endpointList)
            {
                endpoint.IpAddresses.RemoveAll(ip => true);
                if(request.IpIds.Count()!=0)
                    endpoint.IpAddresses.AddRange(allActiveIpAddresses.Where(ip => request.IpIds.Contains(ip.Id)));
            }
        }

        if (request.IpAreaName != null && request.IpMenuName != null)
        {
            endpointList = await _unitOfWork.GetRepository<Endpoint>().GetAllAsync(
                e => e.AreaName == request.IpAreaName && e.ControllerName == request.IpMenuName, a => a.IpAddresses);
            foreach (var endpoint in endpointList)
            {
                endpoint.IpAddresses.RemoveAll(ip => true);
                if(request.IpIds.Count()!=0)
                    endpoint.IpAddresses.AddRange(allActiveIpAddresses.Where(ip => request.IpIds.Contains(ip.Id)));
            }
        }

        if (request.IpAreaName == null && request.IpMenuName == null && request.EndpointId > 0)
        {
            endpointList.Add(await _unitOfWork.GetRepository<Endpoint>()
                .GetAsync(e => e.Id == request.EndpointId, a => a.IpAddresses));
            foreach (var endpoint in endpointList)
            {
                endpoint.IpAddresses.RemoveAll(ip => true);
                if(request.IpIds.Count()!=0)
                    endpoint.IpAddresses.AddRange(allActiveIpAddresses.Where(ip => request.IpIds.Contains(ip.Id)));
            }
        }

        await _unitOfWork.SaveAsync();
        return new AssignIpListAuthorizeEndpointsCommandResponse
        {
            Result = new Result(ResultStatus.Success, Messages.IpUpdated)
        };
    }
}