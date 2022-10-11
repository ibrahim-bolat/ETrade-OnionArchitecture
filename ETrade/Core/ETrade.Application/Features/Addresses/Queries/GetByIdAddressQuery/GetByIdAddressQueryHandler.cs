using AutoMapper;
using ETrade.Application.Features.Addresses.Constants;
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;

namespace ETrade.Application.Features.Addresses.Queries.GetByIdAddressQuery;

public class GetByIdAddressQueryHandler:IRequestHandler<GetByIdAddressQueryRequest,GetByIdAddressQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdAddressQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetByIdAddressQueryResponse> Handle(GetByIdAddressQueryRequest request, CancellationToken cancellationToken)
    {
        var address =
            await _unitOfWork.AddressRepository.GetAsync(x => x.Id == request.Id && x.IsActive == true);
        if (address != null)
        {
            var addressDto = _mapper.Map<AddressDto>(address);
            return new GetByIdAddressQueryResponse
            {
                Result = new DataResult<AddressDto>(ResultStatus.Success, Messages.AddressAdded, addressDto)
            };
        }
        return new GetByIdAddressQueryResponse
        {
            Result = new DataResult<AddressDto>(ResultStatus.Error, Messages.AddressNotFound,null)
        };
    }
}