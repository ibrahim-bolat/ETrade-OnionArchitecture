using MediatR;

namespace ETrade.Application.Features.Accounts.Queries.VerifyTokenUserQuery;

public class VerifyTokenUserQueryRequest:IRequest<VerifyTokenUserQueryResponse>
{
    public string UserId { get; set; }
    public string Token { get; set; }
}