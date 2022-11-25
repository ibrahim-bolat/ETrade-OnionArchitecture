using ETrade.Application.DTOs.Common;
using MediatR;

namespace ETrade.Application.Features.IpOperations.Queries.GetIpAddressListQuery;

public class GetIpAddressListQueryRequest:IRequest<GetIpAddressListQueryResponse>
{
    public DatatableRequestDto DatatableRequestDto { get; set; }
}