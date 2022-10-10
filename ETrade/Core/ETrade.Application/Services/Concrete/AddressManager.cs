using AutoMapper;
using ETrade.Application.Constants;
using ETrade.Application.DTOs.AddressDtos;
using ETrade.Application.Repositories;
using ETrade.Application.Services.Abstract;
using ETrade.Application.Wrappers.Abstract;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;

namespace ETrade.Application.Services.Concrete;

public class AddressManager : IAddressService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddressManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IResult> AddAsync(AddressDto addressDto, string createdByName)
    {
        var count = await _unitOfWork.AddressRepository.CountAsync(x => x.UserId == addressDto.UserId && x.IsActive);
        if (count > 10)
        {
            return new Result(ResultStatus.Error, Messages.AddressCountMoreThan10);
        }
        if (count > 0)
        {
            if (addressDto.DefaultAddress)
            {
                var addresses = await _unitOfWork.AddressRepository.GetAllAsync(a=>a.UserId==addressDto.UserId);
                foreach (var address in addresses)
                {
                    address.DefaultAddress = false;
                    address.ModifiedByName = createdByName;
                    address.ModifiedTime = DateTime.Now;
                    await _unitOfWork.AddressRepository.UpdateAsync(address);
                }
                var newAddress = _mapper.Map<Address>(addressDto);
                newAddress.DefaultAddress = true;
                newAddress.CreatedByName = createdByName;
                newAddress.ModifiedByName = createdByName;
                newAddress.CreatedTime = DateTime.Now;
                newAddress.ModifiedTime = DateTime.Now;
                newAddress.IsActive = true;
                newAddress.IsDeleted = false;
                await _unitOfWork.AddressRepository.AddAsync(newAddress);
            }
            else
            {
                var newAddress = _mapper.Map<Address>(addressDto);
                newAddress.DefaultAddress = false;
                newAddress.CreatedByName = createdByName;
                newAddress.ModifiedByName = createdByName;
                newAddress.CreatedTime = DateTime.Now;
                newAddress.ModifiedTime = DateTime.Now;
                newAddress.IsActive = true;
                newAddress.IsDeleted = false;
                await _unitOfWork.AddressRepository.AddAsync(newAddress);
            }
        }
        else
        {
            if (addressDto.DefaultAddress)
            {
                var newAddress = _mapper.Map<Address>(addressDto);
                newAddress.DefaultAddress = true;
                newAddress.CreatedByName = createdByName;
                newAddress.ModifiedByName = createdByName;
                newAddress.CreatedTime = DateTime.Now;
                newAddress.ModifiedTime = DateTime.Now;
                newAddress.IsActive = true;
                newAddress.IsDeleted = false;
                await _unitOfWork.AddressRepository.AddAsync(newAddress);
            }
            else
            {
                var newAddress = _mapper.Map<Address>(addressDto);
                newAddress.DefaultAddress = false;
                newAddress.CreatedByName = createdByName;
                newAddress.ModifiedByName = createdByName;
                newAddress.CreatedTime = DateTime.Now;
                newAddress.ModifiedTime = DateTime.Now;
                newAddress.IsActive = true;
                newAddress.IsDeleted = false;
                await _unitOfWork.AddressRepository.AddAsync(newAddress);
            }
        }
        int result = await _unitOfWork.SaveAsync();
        if (result > 0)
            return new DataResult<AddressDto>(ResultStatus.Success, Messages.AddressAdded, addressDto);
        return new Result(ResultStatus.Error, Messages.AddressNotAdded);
    }

    public async Task<IResult> UpdateAsync(AddressDto addressDto, string modifiedByName)
    {
        if (addressDto.DefaultAddress)
        {
            var addresses = await _unitOfWork.AddressRepository.GetAllAsync(a=>a.UserId==addressDto.UserId);
            for (int i = 0; i < addresses.Count; i++)
            {
   
                if (addresses[i].Id != addressDto.Id)
                {
                        addresses[i].DefaultAddress = false;
                        addresses[i].ModifiedByName = modifiedByName;
                        addresses[i].ModifiedTime = DateTime.Now;
                        await _unitOfWork.AddressRepository.UpdateAsync(addresses[i]);
                }
                else
                {
                        addresses[i] = _mapper.Map(addressDto, addresses[i]);
                        addresses[i].DefaultAddress = true;
                        addresses[i].ModifiedByName = modifiedByName;
                        addresses[i].ModifiedTime = DateTime.Now;
                        await _unitOfWork.AddressRepository.UpdateAsync(addresses[i]);
                }
            }
        }
        else
        {
            var address = await _unitOfWork.AddressRepository.GetAsync(x => x.Id == addressDto.Id);
            if (address != null)
            {
                address = _mapper.Map(addressDto, address);
                address.DefaultAddress = false;
                address.ModifiedByName = modifiedByName;
                address.ModifiedTime = DateTime.Now;
                await _unitOfWork.AddressRepository.UpdateAsync(address);
            }
        }
        var result = await _unitOfWork.SaveAsync();
        if (result > 0)
                return new DataResult<AddressDto>(ResultStatus.Success, Messages.AddressUpdated, addressDto);
        return new Result(ResultStatus.Error, Messages.AddressNotUpdated);
    }


    public async Task<IDataResult<AddressDto>> DeleteAsync(int id, string modifiedByName)
    {
        var address = await _unitOfWork.AddressRepository.GetAsync(x => x.Id == id);
        if (address != null)
        {
            address.IsActive = false;
            address.IsDeleted = true;
            address.ModifiedByName = modifiedByName;
            address.ModifiedTime = DateTime.Now;
            var deletedAddress = _unitOfWork.AddressRepository.UpdateAsync(address);
            var result = await _unitOfWork.SaveAsync();
            var addressDto = _mapper.Map<AddressDto>(address);
            if (result > 0)
                return new DataResult<AddressDto>(ResultStatus.Success, Messages.AddressDeleted,addressDto);
            return new DataResult<AddressDto>(ResultStatus.Error, Messages.AddressNotDeleted, null);
        }
        return new DataResult<AddressDto>(ResultStatus.Error, Messages.NotFound, null);
    }

    public async Task<IDataResult<AddressDto>> GetAsync(int id)
    {
        var address =
            await _unitOfWork.AddressRepository.GetAsync(x => x.Id == id && x.IsActive == true);
        if (address != null)
        {
            var addressDto = _mapper.Map<AddressDto>(address);
            return new DataResult<AddressDto>(ResultStatus.Success, addressDto);
        }

        return new DataResult<AddressDto>(ResultStatus.Error, Messages.NotFound, null);
    }
}