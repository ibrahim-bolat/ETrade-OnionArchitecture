using ETrade.Application.DTOs.Common;
using MediatR;

namespace ETrade.Application.Features.Addresses.Queries.GetCityListQuery;

public class GetCityListQueryRequest:IRequest<GetCityListQueryResponse>
{
}