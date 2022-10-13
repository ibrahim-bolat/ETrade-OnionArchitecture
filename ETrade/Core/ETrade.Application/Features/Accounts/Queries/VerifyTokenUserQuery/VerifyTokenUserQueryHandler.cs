using System.Web;
using ETrade.Application.Features.Accounts.Constants;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.Accounts.Queries.VerifyTokenUserQuery;

public class VerifyTokenUserQueryHandler:IRequestHandler<VerifyTokenUserQueryRequest,VerifyTokenUserQueryResponse>
{
    private readonly UserManager<AppUser> _userManager;

    public VerifyTokenUserQueryHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;

    }

    public async Task<VerifyTokenUserQueryResponse> Handle(VerifyTokenUserQueryRequest request, CancellationToken cancellationToken)
    {
        AppUser user = await _userManager.FindByIdAsync(request.UserId);
        if (user != null)
        {
            string verifyToken = HttpUtility.UrlDecode(request.Token);
            bool result = await  _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider,
                "ResetPassword", verifyToken);
            if (result)
            {
                return new VerifyTokenUserQueryResponse
                {
                    Result = new Result(ResultStatus.Success, Messages.UserSuccessVerifyToken)
                };
            }
            return new VerifyTokenUserQueryResponse{
                Result = new Result(ResultStatus.Error, Messages.UserErrorVerifyToken)
            };
        }
        return new VerifyTokenUserQueryResponse{
            Result = new Result(ResultStatus.Error, Messages.UserNotFound)
        };
    }
}