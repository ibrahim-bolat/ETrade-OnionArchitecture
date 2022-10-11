using AutoMapper;
using ETrade.Application.Features.Addresses.Constants;
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;

namespace ETrade.Application.Features.Addresses.Commands.DeleteAddressCommand;

public class CreateAddressCommandHandler:IRequestHandler<DeleteAddressCommandRequest,DeleteAddressCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DeleteAddressCommandResponse> Handle(DeleteAddressCommandRequest request, CancellationToken cancellationToken)
    {
        var address = await _unitOfWork.AddressRepository.GetAsync(x => x.Id == request.Id);
        if (address != null)
        {
            address.IsActive = false;
            address.IsDeleted = true;
            address.ModifiedByName = request.ModifiedByName;
            address.ModifiedTime = DateTime.Now;
            var deletedAddress = _unitOfWork.AddressRepository.UpdateAsync(address);
            var result = await _unitOfWork.SaveAsync();
            var addressDto = _mapper.Map<AddressDto>(address);
            if (result > 0)
            {
                return new DeleteAddressCommandResponse
                {
                    Result = new DataResult<AddressDto>(ResultStatus.Success, Messages.AddressDeleted, addressDto)
                };
            }
            return new DeleteAddressCommandResponse
            {
                Result = new DataResult<AddressDto>(ResultStatus.Error, Messages.AddressNotDeleted, null)
            };
        }
        return new DeleteAddressCommandResponse
        {
            Result = new DataResult<AddressDto>(ResultStatus.Error, Messages.AddressNotFound, null)
        };
    }
}