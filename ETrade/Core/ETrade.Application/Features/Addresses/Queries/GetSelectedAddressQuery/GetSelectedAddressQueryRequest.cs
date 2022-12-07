using ETrade.Application.Features.Addresses.DTOs;
using MediatR;

namespace ETrade.Application.Features.Addresses.Queries.GetSelectedAddressQuery;

public class GetSelectedAddressQueryRequest:IRequest<GetSelectedAddressQueryResponse>
{
    public CreateAddressDto CreateAddressDto { get; set; }
}