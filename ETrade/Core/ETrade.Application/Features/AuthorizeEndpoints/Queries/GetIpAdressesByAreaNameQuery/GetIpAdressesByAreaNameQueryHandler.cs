using AutoMapper;
using ETrade.Application.Extensions;
using ETrade.Application.Features.AuthorizeEndpoints.DTOs;
using ETrade.Application.Features.IpOperations.Constants;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetIpAdressesByAreaNameQuery;

public class GetIpAdressesByAreaNameQueryHandler:IRequestHandler<GetIpAdressesByAreaNameQueryRequest,GetIpAdressesByAreaNameQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetIpAdressesByAreaNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetIpAdressesByAreaNameQueryResponse> Handle(GetIpAdressesByAreaNameQueryRequest request, CancellationToken cancellationToken)
    {
        /*
    if (request.Id.StartsWith("a"))
 {
     int endpointId= Convert.ToInt32(request.Id.Substring(1));
     List<Endpoint> endpoints= await _unitOfWork.GetRepository<Endpoint>().GetAllAsync(a=>a.Id==endpointId);
 }
  */
        return null;
    }
}