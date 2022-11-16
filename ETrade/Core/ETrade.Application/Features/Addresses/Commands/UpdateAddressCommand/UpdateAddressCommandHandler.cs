using AutoMapper;
using ETrade.Application.Features.Addresses.Constants;
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;

namespace ETrade.Application.Features.Addresses.Commands.UpdateAddressCommand;

public class UpdateAddressCommandHandler:IRequestHandler<UpdateAddressCommandRequest,UpdateAddressCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UpdateAddressCommandResponse> Handle(UpdateAddressCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.AddressDto.DefaultAddress)
        {
            var addresses = await _unitOfWork.GetRepository<Address>().GetAllAsync(a=>a.UserId==request.AddressDto.UserId);
            for (int i = 0; i < addresses.Count; i++)
            {
   
                if (addresses[i].Id != request.AddressDto.Id)
                {
                    addresses[i].DefaultAddress = false;
                    addresses[i].ModifiedByName = request.ModifiedByName;
                    addresses[i].ModifiedTime = DateTime.Now;
                    await _unitOfWork.GetRepository<Address>().UpdateAsync(addresses[i]);
                }
                else
                {
                    addresses[i] = _mapper.Map(request.AddressDto, addresses[i]);
                    addresses[i].DefaultAddress = true;
                    addresses[i].ModifiedByName = request.ModifiedByName;
                    addresses[i].ModifiedTime = DateTime.Now;
                    await _unitOfWork.GetRepository<Address>().UpdateAsync(addresses[i]);
                }
            }
        }
        else
        {
            var address = await _unitOfWork.GetRepository<Address>().GetAsync(x => x.Id == request.AddressDto.Id);
            if (address != null)
            {
                address = _mapper.Map(request.AddressDto, address);
                address.DefaultAddress = false;
                address.ModifiedByName = request.ModifiedByName;
                address.ModifiedTime = DateTime.Now;
                await _unitOfWork.GetRepository<Address>().UpdateAsync(address);
            }
        }
        var result = await _unitOfWork.SaveAsync();
        if (result > 0)
        {
            return new UpdateAddressCommandResponse
            {
                Result = new DataResult<AddressDto>(ResultStatus.Success, Messages.AddressUpdated, request.AddressDto)
            };
        }
        return new UpdateAddressCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.AddressNotUpdated)
        };
    }
}