using MediatR;

namespace ETrade.Application.Features.Addresses.Queries.GetByIdUpdateAddressQuery;

public class GetByIdUpdateAddressQueryRequest:IRequest<GetByIdUpdateAddressQueryResponse>
{
    public int Id { get; set; }
}