using MediatR;

namespace ETrade.Application.Features.Addresses.Queries.GetCreateAddressQuery;

public class GetCreateAddressQueryRequest:IRequest<GetCreateAddressQueryResponse>
{
    public int UserId { get; set; }
}