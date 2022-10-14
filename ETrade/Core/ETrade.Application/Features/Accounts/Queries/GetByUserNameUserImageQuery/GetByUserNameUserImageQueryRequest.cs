using MediatR;

namespace ETrade.Application.Features.Accounts.Queries.GetByUserNameUserImageQuery;

public class GetByUserNameUserImageQueryRequest:IRequest<GetByUserNameUserImageQueryResponse>
{
    public string UserName { get; set; }
}