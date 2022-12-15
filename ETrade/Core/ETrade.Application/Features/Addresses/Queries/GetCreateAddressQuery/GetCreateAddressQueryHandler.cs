using AutoMapper;
using ETrade.Application.Features.Addresses.Constants;
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ETrade.Application.Features.Addresses.Queries.GetCreateAddressQuery;

public class GetCreateAddressQueryHandler:IRequestHandler<GetCreateAddressQueryRequest,GetCreateAddressQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCreateAddressQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetCreateAddressQueryResponse> Handle(GetCreateAddressQueryRequest request,
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
            AddressDto addressDto = new AddressDto()
            {
                UserId = request.UserId,
                Cities = citySelectListItems,
            };
            return new GetCreateAddressQueryResponse
            {
                Result = new DataResult<AddressDto>(ResultStatus.Success, addressDto)
            };
        }
        return new GetCreateAddressQueryResponse
        {
            Result = new DataResult<AddressDto>(ResultStatus.Error, Messages.AddressNotFound,null)
        };
    }
}