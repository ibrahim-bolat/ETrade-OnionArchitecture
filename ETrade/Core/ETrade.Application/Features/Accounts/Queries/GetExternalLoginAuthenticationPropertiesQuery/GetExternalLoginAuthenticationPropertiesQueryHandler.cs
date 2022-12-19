using ETrade.Application.Constants;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.Accounts.Queries.GetExternalLoginAuthenticationPropertiesQuery;

public class GetExternalLoginAuthenticationPropertiesQueryHandler:IRequestHandler<GetExternalLoginAuthenticationPropertiesQueryRequest,GetExternalLoginAuthenticationPropertiesQueryResponse>
{

    private readonly SignInManager<AppUser> _signInManager;

    public GetExternalLoginAuthenticationPropertiesQueryHandler(SignInManager<AppUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public Task<GetExternalLoginAuthenticationPropertiesQueryResponse> Handle(GetExternalLoginAuthenticationPropertiesQueryRequest request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.ProviderName) && !string.IsNullOrEmpty(request.RedirectUrl))
        {
            AuthenticationProperties properties = _signInManager.ConfigureExternalAuthenticationProperties(request.ProviderName.Trim(), request.RedirectUrl);
            return Task.FromResult(new GetExternalLoginAuthenticationPropertiesQueryResponse
            {
                Result =  new DataResult<AuthenticationProperties>(ResultStatus.Success,properties )
            });
        }
        return Task.FromResult(new GetExternalLoginAuthenticationPropertiesQueryResponse{
            Result = new DataResult<AuthenticationProperties>(ResultStatus.Error, Messages.AuthenticationPropertiesNotFound,null)
        });
    }
}