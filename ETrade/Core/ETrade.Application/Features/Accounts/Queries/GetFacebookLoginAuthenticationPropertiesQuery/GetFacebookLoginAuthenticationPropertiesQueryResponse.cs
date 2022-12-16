
using ETrade.Application.Wrappers.Abstract;
using Microsoft.AspNetCore.Authentication;

namespace ETrade.Application.Features.Accounts.Queries.GetFacebookLoginAuthenticationPropertiesQuery;

public class GetFacebookLoginAuthenticationPropertiesQueryResponse
{
    public IDataResult<AuthenticationProperties> Result { get; set; }
}