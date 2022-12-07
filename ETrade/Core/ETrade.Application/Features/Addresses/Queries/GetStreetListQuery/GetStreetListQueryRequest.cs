using MediatR;

namespace ETrade.Application.Features.Addresses.Queries.GetStreetListQuery;

public class GetStreetListQueryRequest:IRequest<GetStreetListQueryResponse>
{
    public int NeighborhoodOrVillageId { get; set; }
}