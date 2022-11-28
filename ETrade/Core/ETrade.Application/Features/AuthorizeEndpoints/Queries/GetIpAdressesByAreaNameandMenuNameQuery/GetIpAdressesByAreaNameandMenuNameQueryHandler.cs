using AutoMapper;
using ETrade.Application.Extensions;
using ETrade.Application.Features.AuthorizeEndpoints.DTOs;
using ETrade.Application.Features.IpOperations.Constants;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;

namespace ETrade.Application.Features.AuthorizeEndpoints.Queries.GetIpAdressesByAreaNameandMenuNameQuery;

public class GetIpAdressesByAreaNameandMenuNameQueryHandler:IRequestHandler<GetIpAdressesByAreaNameandMenuNameQueryRequest,GetIpAdressesByAreaNameandMenuNameQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetIpAdressesByAreaNameandMenuNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetIpAdressesByAreaNameandMenuNameQueryResponse> Handle(GetIpAdressesByAreaNameandMenuNameQueryRequest request, CancellationToken cancellationToken)
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