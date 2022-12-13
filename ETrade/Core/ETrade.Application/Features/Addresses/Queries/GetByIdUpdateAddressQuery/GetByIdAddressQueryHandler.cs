using AutoMapper;
using ETrade.Application.Features.Addresses.Constants;
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Application.Features.Addresses.Queries.GetByIdUpdateAddressQuery;

public class GetByIdUpdateAddressQueryHandler : IRequestHandler<GetByIdUpdateAddressQueryRequest, GetByIdUpdateAddressQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdUpdateAddressQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetByIdUpdateAddressQueryResponse> Handle(GetByIdUpdateAddressQueryRequest request,
        CancellationToken cancellationToken)
    {
        var address =
            await _unitOfWork.GetRepository<Address>().GetAsync(predicate:x => x.Id == request.Id && x.IsActive == true);
        List<City> cityList;
        if (address is not null)
        {
            if (address.StreetName is not null)
            {
                cityList = await _unitOfWork.GetRepository<City>().GetAllAsync(
                    include: c => c
                        .Include(city => city.Districts.Where(d => d.CityId == Convert.ToInt32(address.CityId)))
                        .ThenInclude(district =>
                            district.NeighborhoodsOrVillages.Where(n =>
                                n.DistrictId == Convert.ToInt32(address.DistrictId)))
                        .ThenInclude(neighborhoodorvillage =>
                            neighborhoodorvillage.Streets.Where(s =>
                                s.Id == Convert.ToInt32(address.NeighborhoodOrVillageId))));
            }
            else
            {
                cityList = await _unitOfWork.GetRepository<City>().GetAllAsync(
                    include: c => c
                        .Include(city => city.Districts.Where(d => d.CityId == Convert.ToInt32(address.CityId)))
                        .ThenInclude(district =>
                            district.NeighborhoodsOrVillages.Where(n =>
                                n.DistrictId == Convert.ToInt32(address.DistrictId))));
            }
            AddressDto addressDto = new AddressDto();
            addressDto = _mapper.Map(address, addressDto);
            addressDto.Cities = cityList.Select(city => new SelectListItem()
            {
                Value = city.Id.ToString(),
                Text = city.Name,
            }).ToList();
            var districtList = await _unitOfWork.GetRepository<District>()
                .GetAllAsync(predicate:district => district.CityId.ToString().Equals(addressDto.CityId));
            if (districtList != null)
            {
                addressDto.Districts = districtList.Select(district => new SelectListItem()
                {
                    Value = district.Id.ToString(),
                    Text = district.Name,
                }).ToList();

                var neighborhoodorvillageList = await _unitOfWork.GetRepository<NeighborhoodOrVillage>()
                    .GetAllAsync(predicate:neighborhoodOrVillage =>
                        neighborhoodOrVillage.DistrictId.ToString().Equals(addressDto.DistrictId));
                if (neighborhoodorvillageList != null)
                {
                    addressDto.NeighborhoodsOrVillages = neighborhoodorvillageList.Select(neighborhoodorvillage =>
                        new SelectListItem()
                        {
                            Value = neighborhoodorvillage.Id.ToString(),
                            Text = neighborhoodorvillage.Name,
                        }).ToList();

                    if (!string.IsNullOrEmpty(addressDto.StreetId))
                    {
                        var streetList = await _unitOfWork.GetRepository<Street>().GetAllAsync(predicate:street =>
                            street.NeighborhoodOrVillageId.ToString().Equals(addressDto.NeighborhoodOrVillageId));
                        if (streetList != null)
                        {
                            addressDto.Streets = streetList.Select(street => new SelectListItem()
                            {
                                Value = street.Id.ToString(),
                                Text = street.Name,
                            }).ToList();
                        }
                    }
                }
            }

            return new GetByIdUpdateAddressQueryResponse
            {
                Result = new DataResult<AddressDto>(ResultStatus.Success, addressDto)
            };
        }

        return new GetByIdUpdateAddressQueryResponse
        {
            Result = new DataResult<AddressDto>(ResultStatus.Error, Messages.AddressNotFound, null)
        };
    }
}