using AutoMapper;
using ETrade.Application.Features.Addresses.Constants;
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ETrade.Application.Features.Addresses.Queries.GetSelectedAddressQuery;

public class GetSelectedAddressQueryHandler:IRequestHandler<GetSelectedAddressQueryRequest,GetSelectedAddressQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSelectedAddressQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetSelectedAddressQueryResponse> Handle(GetSelectedAddressQueryRequest request,
        CancellationToken cancellationToken)
    {
        var cityList = await _unitOfWork.GetRepository<City>().GetAllAsync();
        if (cityList != null)
        {
            request.AddressDto.Cities = cityList.Select(city => new SelectListItem()
            {
                Value = city.Id.ToString(),
                Text = city.Name,
            }).ToList();
            if (!string.IsNullOrEmpty(request.AddressDto.CityId))
            {
                var districtList = await _unitOfWork.GetRepository<District>()
                    .GetAllAsync(predicate: district => district.CityId == Convert.ToInt32(request.AddressDto.CityId));
                if (districtList != null)
                {
                    request.AddressDto.Districts = districtList.Select(district => new SelectListItem()
                    {
                        Value = district.Id.ToString(),
                        Text = district.Name,
                    }).ToList();

                    if (!string.IsNullOrEmpty(request.AddressDto.DistrictId))
                    {
                        var neighborhoodorvillageList = await _unitOfWork.GetRepository<NeighborhoodOrVillage>()
                            .GetAllAsync(predicate: neighborhoodOrVillage =>
                                neighborhoodOrVillage.DistrictId == Convert.ToInt32(request.AddressDto.DistrictId));
                        if (neighborhoodorvillageList != null)
                        {
                            request.AddressDto.NeighborhoodsOrVillages = neighborhoodorvillageList.Select(
                                neighborhoodorvillage => new SelectListItem()
                                {
                                    Value = neighborhoodorvillage.Id.ToString(),
                                    Text = neighborhoodorvillage.Name,
                                }).ToList();

                            if (!string.IsNullOrEmpty(request.AddressDto.NeighborhoodOrVillageId))
                            {
                                var streetList = await _unitOfWork.GetRepository<Street>().GetAllAsync(
                                    predicate: street =>
                                        street.NeighborhoodOrVillageId ==
                                        Convert.ToInt32(request.AddressDto.NeighborhoodOrVillageId));
                                if (streetList != null)
                                {
                                    request.AddressDto.Streets = streetList.Select(street => new SelectListItem()
                                    {
                                        Value = street.Id.ToString(),
                                        Text = street.Name,
                                    }).ToList();
                                }
                            }
                        }
                    }
                }
            }

            return new GetSelectedAddressQueryResponse
            {
                Result = new DataResult<AddressDto>(ResultStatus.Success, request.AddressDto)
            };
        }
        return new GetSelectedAddressQueryResponse
        {
            Result = new DataResult<AddressDto>(ResultStatus.Error, Messages.AddressNotFound,null)
        };
    }
}