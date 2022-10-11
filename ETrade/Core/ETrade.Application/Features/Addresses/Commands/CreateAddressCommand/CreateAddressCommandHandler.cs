using AutoMapper;
using ETrade.Application.Features.Addresses.Constants;
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;

namespace ETrade.Application.Features.Addresses.Commands.CreateAddressCommand;

public class CreateAddressCommandHandler:IRequestHandler<CreateAddressCommandRequest,CreateAddressCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateAddressCommandResponse> Handle(CreateAddressCommandRequest request, CancellationToken cancellationToken)
    {
       var count = await _unitOfWork.AddressRepository.CountAsync(x => x.UserId == request.AddressDto.UserId && x.IsActive);
        if (count > 4)
        {
            return new CreateAddressCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.AddressCountMoreThan4)
            };
        }
        if (count > 0)
        {
            if (request.AddressDto.DefaultAddress)
            {
                var addresses = await _unitOfWork.AddressRepository.GetAllAsync(a=>a.UserId==request.AddressDto.UserId);
                foreach (var address in addresses)
                {
                    address.DefaultAddress = false;
                    address.ModifiedByName = request.CreatedByName;
                    address.ModifiedTime = DateTime.Now;
                    await _unitOfWork.AddressRepository.UpdateAsync(address);
                }
                var newAddress = _mapper.Map<Address>(request.AddressDto);
                newAddress.DefaultAddress = true;
                newAddress.CreatedByName = request.CreatedByName;
                newAddress.ModifiedByName = request.CreatedByName;
                newAddress.CreatedTime = DateTime.Now;
                newAddress.ModifiedTime = DateTime.Now;
                newAddress.IsActive = true;
                newAddress.IsDeleted = false;
                await _unitOfWork.AddressRepository.AddAsync(newAddress);
            }
            else
            {
                var newAddress = _mapper.Map<Address>(request.AddressDto);
                newAddress.DefaultAddress = false;
                newAddress.CreatedByName = request.CreatedByName;;
                newAddress.ModifiedByName = request.CreatedByName;;
                newAddress.CreatedTime = DateTime.Now;
                newAddress.ModifiedTime = DateTime.Now;
                newAddress.IsActive = true;
                newAddress.IsDeleted = false;
                await _unitOfWork.AddressRepository.AddAsync(newAddress);
            }
        }
        else
        {
            if (request.AddressDto.DefaultAddress)
            {
                var newAddress = _mapper.Map<Address>(request.AddressDto);
                newAddress.DefaultAddress = true;
                newAddress.CreatedByName = request.CreatedByName;;
                newAddress.ModifiedByName = request.CreatedByName;;
                newAddress.CreatedTime = DateTime.Now;
                newAddress.ModifiedTime = DateTime.Now;
                newAddress.IsActive = true;
                newAddress.IsDeleted = false;
                await _unitOfWork.AddressRepository.AddAsync(newAddress);
            }
            else
            {
                var newAddress = _mapper.Map<Address>(request.AddressDto);
                newAddress.DefaultAddress = false;
                newAddress.CreatedByName = request.CreatedByName;;
                newAddress.ModifiedByName = request.CreatedByName;;
                newAddress.CreatedTime = DateTime.Now;
                newAddress.ModifiedTime = DateTime.Now;
                newAddress.IsActive = true;
                newAddress.IsDeleted = false;
                await _unitOfWork.AddressRepository.AddAsync(newAddress);
            }
        }
        int result = await _unitOfWork.SaveAsync();
        if (result > 0)
        {
            return new CreateAddressCommandResponse
            {
                Result = new DataResult<AddressDto>(ResultStatus.Success, Messages.AddressAdded, request.AddressDto)
            };
        }
        return new CreateAddressCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.AddressNotAdded)
        };
    }
}