using AutoMapper;
using ETrade.Application.Features.Addresses.Constants;
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ETrade.Application.Features.Addresses.Commands.CreateAddressCommand;

public class CreateAddressCommandHandler:IRequestHandler<CreateAddressCommandRequest,CreateAddressCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CreateAddressCommandResponse> Handle(CreateAddressCommandRequest request, CancellationToken cancellationToken)
    {
       var count = await _unitOfWork.GetRepository<Address>().CountAsync(x => x.UserId == request.AddressDto.UserId && x.IsActive);
       string createdByName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
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
                var addresses = await _unitOfWork.GetRepository<Address>().GetAllAsync(a=>a.UserId==request.AddressDto.UserId);
                foreach (var address in addresses)
                {
                    address.DefaultAddress = false;
                    address.ModifiedByName = createdByName;
                    address.ModifiedTime = DateTime.Now;
                    await _unitOfWork.GetRepository<Address>().UpdateAsync(address);
                }
                var newAddress = _mapper.Map<Address>(request.AddressDto);
                newAddress.DefaultAddress = true;
                newAddress.CreatedByName = createdByName;
                newAddress.ModifiedByName = createdByName;
                newAddress.CreatedTime = DateTime.Now;
                newAddress.ModifiedTime = DateTime.Now;
                newAddress.IsActive = true;
                newAddress.IsDeleted = false;
                await _unitOfWork.GetRepository<Address>().AddAsync(newAddress);
            }
            else
            {
                var newAddress = _mapper.Map<Address>(request.AddressDto);
                newAddress.DefaultAddress = false;
                newAddress.CreatedByName = createdByName;
                newAddress.ModifiedByName = createdByName;
                newAddress.CreatedTime = DateTime.Now;
                newAddress.ModifiedTime = DateTime.Now;
                newAddress.IsActive = true;
                newAddress.IsDeleted = false;
                await _unitOfWork.GetRepository<Address>().AddAsync(newAddress);
            }
        }
        else
        {
            if (request.AddressDto.DefaultAddress)
            {
                var newAddress = _mapper.Map<Address>(request.AddressDto);
                newAddress.DefaultAddress = true;
                newAddress.CreatedByName = createdByName;
                newAddress.ModifiedByName = createdByName;
                newAddress.CreatedTime = DateTime.Now;
                newAddress.ModifiedTime = DateTime.Now;
                newAddress.IsActive = true;
                newAddress.IsDeleted = false;
                await _unitOfWork.GetRepository<Address>().AddAsync(newAddress);
            }
            else
            {
                var newAddress = _mapper.Map<Address>(request.AddressDto);
                newAddress.DefaultAddress = false;
                newAddress.CreatedByName = createdByName;
                newAddress.ModifiedByName = createdByName;
                newAddress.CreatedTime = DateTime.Now;
                newAddress.ModifiedTime = DateTime.Now;
                newAddress.IsActive = true;
                newAddress.IsDeleted = false;
                await _unitOfWork.GetRepository<Address>().AddAsync(newAddress);
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