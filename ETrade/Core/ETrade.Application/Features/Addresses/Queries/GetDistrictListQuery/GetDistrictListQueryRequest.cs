using MediatR;

namespace ETrade.Application.Features.Addresses.Queries.GetDistrictListQuery;

public class GetDistrictListQueryRequest:IRequest<GetDistrictListQueryResponse>
{
    public int CityId { get; set; }
}