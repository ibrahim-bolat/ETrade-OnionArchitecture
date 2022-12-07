using AutoMapper;
using ETrade.Application.DTOs.Common;
using ETrade.Application.Features.Addresses.Constants;
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Features.RoleOperations.DTOs;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ETrade.Application.Features.Addresses.Queries.GetCityListQuery;

public class GetCityListQueryHandler:IRequestHandler<GetCityListQueryRequest,GetCityListQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCityListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetCityListQueryResponse> Handle(GetCityListQueryRequest request, CancellationToken cancellationToken)
    {
        var cityList = await _unitOfWork.GetRepository<City>().GetAllAsync();
        if (cityList != null)
        {
            var response = cityList.Select(city => new SelectListItem()
            {
                Value = city.Id.ToString(),
                Text = city.Name
            }).ToList();
            return new GetCityListQueryResponse{
                Result = new DataResult<List<SelectListItem>>(ResultStatus.Success, response)
            };
        }
        return new GetCityListQueryResponse{
            Result = new DataResult<List<SelectListItem>>(ResultStatus.Error,Messages.CityNotFound,null)
        };
    }
}