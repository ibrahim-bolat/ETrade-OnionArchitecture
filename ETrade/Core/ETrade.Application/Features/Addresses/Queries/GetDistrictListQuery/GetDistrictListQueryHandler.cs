using AutoMapper;
using ETrade.Application.Features.Addresses.Constants;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ETrade.Application.Features.Addresses.Queries.GetDistrictListQuery;

public class GetDistrictListQueryHandler:IRequestHandler<GetDistrictListQueryRequest,GetDistrictListQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDistrictListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetDistrictListQueryResponse> Handle(GetDistrictListQueryRequest request, CancellationToken cancellationToken)
    {
        var districtList = await _unitOfWork.GetRepository<District>().GetAllAsync(predicate:district=>district.CityId==request.CityId);
        if (districtList != null)
        {
            var response = districtList.Select(district => new SelectListItem()
            {
                Value = district.Id.ToString(),
                Text = district.Name
            }).ToList();
            return new GetDistrictListQueryResponse{
                Result = new DataResult<List<SelectListItem>>(ResultStatus.Success, response)
            };
        }
        return new GetDistrictListQueryResponse{
            Result = new DataResult<List<SelectListItem>>(ResultStatus.Error, Messages.DistrictNotFound,null)
        };
    }
}