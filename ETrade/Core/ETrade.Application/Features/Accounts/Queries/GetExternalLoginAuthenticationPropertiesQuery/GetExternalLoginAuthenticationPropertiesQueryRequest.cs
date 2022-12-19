using MediatR;

namespace ETrade.Application.Features.Accounts.Queries.GetExternalLoginAuthenticationPropertiesQuery;

public class GetExternalLoginAuthenticationPropertiesQueryRequest:IRequest<GetExternalLoginAuthenticationPropertiesQueryResponse>
{
    public string ProviderName { get; set; }
    public string RedirectUrl { get; set; }
}