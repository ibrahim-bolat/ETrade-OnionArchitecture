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
    private readonly IMapper _mapper;

    public GetSelectedAddressQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetSelectedAddressQueryResponse> Handle(GetSelectedAddressQueryRequest request,
        CancellationToken cancellationToken)
    {
        var cityList = await _unitOfWork.GetRepository<City>().GetAllAsync();
        if (cityList != null)
        {
            request.CreateAddressDto.Cities = cityList.Select(city => new SelectListItem()
            {
                Value = city.Id.ToString(),
                Text = city.Name,
            }).ToList();
            var districtList = await _unitOfWork.GetRepository<District>().GetAllAsync(district=>district.CityId==Convert.ToInt32(request.CreateAddressDto.CityId));
            if (districtList != null)
            {
                request.CreateAddressDto.Districts  = districtList.Select(district => new SelectListItem()
                {
                    Value = district.Id.ToString(),
                    Text = district.Name,
                }).ToList();
                
                var neighborhoodorvillageList = await _unitOfWork.GetRepository<NeighborhoodOrVillage>().GetAllAsync(neighborhoodOrVillage=>neighborhoodOrVillage.DistrictId==Convert.ToInt32(request.CreateAddressDto.DistrictId));
                if (neighborhoodorvillageList != null)
                {
                    request.CreateAddressDto.NeighborhoodsOrVillages  = neighborhoodorvillageList.Select(neighborhoodorvillage => new SelectListItem()
                    {
                        Value = neighborhoodorvillage.Id.ToString(),
                        Text = neighborhoodorvillage.Name,
                    }).ToList();
                    
                    if (!string.IsNullOrEmpty(request.CreateAddressDto.StreetId))
                    {
                        var streetList = await _unitOfWork.GetRepository<Street>().GetAllAsync(street=>street.NeighborhoodOrVillageId==Convert.ToInt32(request.CreateAddressDto.NeighborhoodOrVillageId));
                        if (streetList != null)
                        {
                            request.CreateAddressDto.Streets  = streetList.Select(street => new SelectListItem()
                            {
                                Value = street.Id.ToString(),
                                Text = street.Name,
                            }).ToList();
                        }
                    }
                }
            }
            return new GetSelectedAddressQueryResponse
            {
                Result = new DataResult<CreateAddressDto>(ResultStatus.Success, request.CreateAddressDto)
            };
        }
        return new GetSelectedAddressQueryResponse
        {
            Result = new DataResult<CreateAddressDto>(ResultStatus.Error, Messages.AddressNotFound,null)
        };
    }
}