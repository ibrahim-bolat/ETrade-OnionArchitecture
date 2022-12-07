using MediatR;

namespace ETrade.Application.Features.Addresses.Queries.GetCreateAddressDtoQuery;

public class GetCreateAddressDtoQueryRequest:IRequest<GetCreateAddressDtoQueryResponse>
{
    public int UserId { get; set; }
}