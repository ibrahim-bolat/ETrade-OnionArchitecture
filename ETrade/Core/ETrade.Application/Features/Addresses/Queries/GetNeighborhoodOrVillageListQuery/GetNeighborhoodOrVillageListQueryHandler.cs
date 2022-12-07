using AutoMapper;
using ETrade.Application.Features.Addresses.Constants;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ETrade.Application.Features.Addresses.Queries.GetNeighborhoodOrVillageListQuery;

public class GetNeighborhoodOrVillageListQueryHandler:IRequestHandler<GetNeighborhoodOrVillageListQueryRequest,GetNeighborhoodOrVillageListQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetNeighborhoodOrVillageListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetNeighborhoodOrVillageListQueryResponse> Handle(GetNeighborhoodOrVillageListQueryRequest request, CancellationToken cancellationToken)
    {
        var neighborhoodorvillageList = await _unitOfWork.GetRepository<NeighborhoodOrVillage>().GetAllAsync(neighborhoodOrVillage=>neighborhoodOrVillage.DistrictId==request.DistrictId);
        if (neighborhoodorvillageList != null)
        {
            var response = neighborhoodorvillageList.Select(neighborhoodorvillage => new SelectListItem()
            {
                Value = neighborhoodorvillage.Id.ToString(),
                Text = neighborhoodorvillage.Name
            }).ToList();
            return new GetNeighborhoodOrVillageListQueryResponse{
                Result = new DataResult<List<SelectListItem>>(ResultStatus.Success, response)
            };
        }
        return new GetNeighborhoodOrVillageListQueryResponse{
            Result = new DataResult<List<SelectListItem>>(ResultStatus.Error, Messages.NeighborhoodOrVillageNotFound,null)
        };
    }
}