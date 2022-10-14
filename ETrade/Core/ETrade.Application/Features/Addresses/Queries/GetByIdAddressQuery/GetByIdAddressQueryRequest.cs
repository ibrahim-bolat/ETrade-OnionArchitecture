using MediatR;

namespace ETrade.Application.Features.Addresses.Queries.GetByIdAddressQuery;

public class GetByIdAddressQueryRequest:IRequest<GetByIdAddressQueryResponse>
{
    public int Id { get; set; }
}