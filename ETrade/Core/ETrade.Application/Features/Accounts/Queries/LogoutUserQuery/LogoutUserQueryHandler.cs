using ETrade.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.Accounts.Queries.LogoutUserQuery;

public class LogoutUserQueryHandler:AsyncRequestHandler<LogoutUserQueryRequest>
{
    private readonly SignInManager<AppUser> _signInManager;

    public LogoutUserQueryHandler(SignInManager<AppUser> signInManager)
    {
        _signInManager = signInManager;
    }


    protected override async Task Handle(LogoutUserQueryRequest request, CancellationToken cancellationToken)
    {
        await _signInManager.SignOutAsync();
    }
}