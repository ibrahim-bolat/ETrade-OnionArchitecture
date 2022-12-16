using ETrade.Application.Constants;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.Accounts.Queries.GetFacebookLoginAuthenticationPropertiesQuery;

public class GetFacebookLoginAuthenticationPropertiesQueryHandler:IRequestHandler<GetFacebookLoginAuthenticationPropertiesQueryRequest,GetFacebookLoginAuthenticationPropertiesQueryResponse>
{

    private readonly SignInManager<AppUser> _signInManager;

    public GetFacebookLoginAuthenticationPropertiesQueryHandler(SignInManager<AppUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public Task<GetFacebookLoginAuthenticationPropertiesQueryResponse> Handle(GetFacebookLoginAuthenticationPropertiesQueryRequest request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.RedirectUrl))
        {
            AuthenticationProperties properties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", request.RedirectUrl);
            return Task.FromResult(new GetFacebookLoginAuthenticationPropertiesQueryResponse
            {
                Result =  new DataResult<AuthenticationProperties>(ResultStatus.Success,properties )
            });
        }
        return Task.FromResult(new GetFacebookLoginAuthenticationPropertiesQueryResponse{
            Result = new DataResult<AuthenticationProperties>(ResultStatus.Error, Messages.AuthenticationPropertiesNotFound,null)
        });
    }
}