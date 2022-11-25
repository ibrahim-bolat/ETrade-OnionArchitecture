using AutoMapper;
using ETrade.Application.Features.IpOperations.Constants;
using ETrade.Application.Features.IpOperations.DTOs;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;

namespace ETrade.Application.Features.IpOperations.Queries.GetByIdIpAddressQuery;

public class GetByIdIpAddressQueryHandler:IRequestHandler<GetByIdIpAddressQueryRequest,GetByIdIpAddressQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdIpAddressQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetByIdIpAddressQueryResponse> Handle(GetByIdIpAddressQueryRequest request, CancellationToken cancellationToken)
    {
        var ipAddress = await _unitOfWork.GetRepository<IpAddress>().GetByIdAsync(request.Id);
        if (ipAddress != null)
        {
            IpDto ipDto = _mapper.Map<IpDto>(ipAddress);
            return new GetByIdIpAddressQueryResponse{
                Result = new DataResult<IpDto>(ResultStatus.Success, ipDto)
            };
        }
        return new GetByIdIpAddressQueryResponse{
            Result = new DataResult<IpDto>(ResultStatus.Error, Messages.IpNotFound,null)
        };
    }
}