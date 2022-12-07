using AutoMapper;
using ETrade.Application.Features.Addresses.Constants;
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ETrade.Application.Features.Addresses.Queries.GetCreateAddressDtoQuery;

public class GetCreateAddressDtoQueryHandler:IRequestHandler<GetCreateAddressDtoQueryRequest,GetCreateAddressDtoQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCreateAddressDtoQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetCreateAddressDtoQueryResponse> Handle(GetCreateAddressDtoQueryRequest request,
        CancellationToken cancellationToken)
    {
        var cityList = await _unitOfWork.GetRepository<City>().GetAllAsync();
        List<SelectListItem> citySelectListItems = null;
        if (cityList != null)
        {
            citySelectListItems = cityList.Select(city => new SelectListItem()
            {
                Value = city.Id.ToString(),
                Text = city.Name
            }).ToList();
            CreateAddressDto createAddressDto = new CreateAddressDto()
            {
                UserId = request.UserId,
                Cities = citySelectListItems,
            };
            return new GetCreateAddressDtoQueryResponse
            {
                Result = new DataResult<CreateAddressDto>(ResultStatus.Success, createAddressDto)
            };
        }
        return new GetCreateAddressDtoQueryResponse
        {
            Result = new DataResult<CreateAddressDto>(ResultStatus.Error, Messages.AddressNotFound,null)
        };
    }
}