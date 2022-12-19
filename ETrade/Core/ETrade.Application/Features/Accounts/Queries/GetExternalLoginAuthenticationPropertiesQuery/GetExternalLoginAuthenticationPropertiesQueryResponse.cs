
using ETrade.Application.Wrappers.Abstract;
using Microsoft.AspNetCore.Authentication;

namespace ETrade.Application.Features.Accounts.Queries.GetExternalLoginAuthenticationPropertiesQuery;

public class GetExternalLoginAuthenticationPropertiesQueryResponse
{
    public IDataResult<AuthenticationProperties> Result { get; set; }
}