using AutoMapper;
using ETrade.Application.Features.Addresses.Constants;
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var addresses = await _unitOfWork.GetRepository<Address>().GetAllAsync(predicate:a=>a.UserId==request.AddressDto.UserId);
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
                    GetNames(request.AddressDto,out string cityName,out string districtName,out string neighborhoodOrVillageName,out string streetName);
                    addresses[i] = _mapper.Map(request.AddressDto, addresses[i]);
                    addresses[i].DefaultAddress = true;
                    addresses[i].ModifiedByName = request.ModifiedByName;
                    addresses[i].ModifiedTime = DateTime.Now;
                    addresses[i].CityId = request.AddressDto.CityId;
                    addresses[i].CityName = cityName;    
                    addresses[i].DistrictId = request.AddressDto.DistrictId;
                    addresses[i].DistrictName = districtName;            
                    addresses[i].NeighborhoodOrVillageId = request.AddressDto.NeighborhoodOrVillageId;
                    addresses[i].NeighborhoodOrVillageName = neighborhoodOrVillageName;                    
                    addresses[i].StreetId = request.AddressDto.StreetId;
                    addresses[i].StreetName = streetName;
                    await _unitOfWork.GetRepository<Address>().UpdateAsync(addresses[i]);
                }
            }
        }
        else
        {
            var address = await _unitOfWork.GetRepository<Address>().GetAsync(predicate:x => x.Id == request.AddressDto.Id);
            if (address != null)
            {
                GetNames(request.AddressDto,out string cityName,out string districtName,out string neighborhoodOrVillageName,out string streetName);
                address = _mapper.Map(request.AddressDto, address);
                address.DefaultAddress = false;
                address.ModifiedByName = request.ModifiedByName;
                address.ModifiedTime = DateTime.Now;
                address.CityId = request.AddressDto.CityId;
                address.CityName = cityName;    
                address.DistrictId = request.AddressDto.DistrictId;
                address.DistrictName = districtName;            
                address.NeighborhoodOrVillageId = request.AddressDto.NeighborhoodOrVillageId;
                address.NeighborhoodOrVillageName = neighborhoodOrVillageName;                    
                address.StreetId = request.AddressDto.StreetId;
                address.StreetName = streetName;
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

    private void GetNames(AddressDto addressDto,out string cityName,out string districtName,out string neighborhoodOrVillageName,out string streetName)
    {
        int cityId = Convert.ToInt32(addressDto.CityId);
        int districtId = Convert.ToInt32(addressDto.DistrictId);
        int neighborhoodorvillageId = Convert.ToInt32(addressDto.NeighborhoodOrVillageId);
        City city = null;
        if (int.TryParse(addressDto.StreetId, out int streetId))
        {
            city =  _unitOfWork.GetRepository<City>().GetAsync(predicate:c => c.Id == cityId,
                include: c => c.Include(city=>city.Districts.Where(d=>d.Id==districtId))
                    .ThenInclude(district=>district.NeighborhoodsOrVillages.Where(n=>n.Id==neighborhoodorvillageId))
                    .ThenInclude(neighborhoodorvillage=>neighborhoodorvillage.Streets.Where(s=>s.Id==streetId))).Result;
            cityName = city.Name;
            districtName = city.Districts.FirstOrDefault()?.Name;
            neighborhoodOrVillageName = city.Districts.FirstOrDefault()?.NeighborhoodsOrVillages.FirstOrDefault()?.Name;
            streetName = city.Districts.FirstOrDefault()?.NeighborhoodsOrVillages.FirstOrDefault()
                ?.Streets.FirstOrDefault()?.Name;
        }
        else
        {
            city =  _unitOfWork.GetRepository<City>().GetAsync(predicate:c => c.Id == cityId,
                include: c => c.Include(city => city.Districts.Where(d => d.Id == districtId))
                    .ThenInclude(district => district.NeighborhoodsOrVillages.Where(n => n.Id == neighborhoodorvillageId))).Result;
            cityName = city.Name;
            districtName = city.Districts.FirstOrDefault()?.Name;
            neighborhoodOrVillageName = city.Districts.FirstOrDefault()?.NeighborhoodsOrVillages.FirstOrDefault()?.Name;
            streetName = null;
        }
    }
}