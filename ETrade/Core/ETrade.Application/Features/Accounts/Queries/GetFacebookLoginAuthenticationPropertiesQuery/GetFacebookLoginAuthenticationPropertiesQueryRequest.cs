using MediatR;

namespace ETrade.Application.Features.Accounts.Queries.GetFacebookLoginAuthenticationPropertiesQuery;

public class GetFacebookLoginAuthenticationPropertiesQueryRequest:IRequest<GetFacebookLoginAuthenticationPropertiesQueryResponse>
{
    public string RedirectUrl { get; set; }
}