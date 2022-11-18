using System.Security.Claims;
using Serilog.Context;

namespace ETrade.MVC.Extensions;

public static class AddUserIdtoSeriLogContextExtension
{
    public static void AddUserIdtoSeriLogContext(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId is not null)
                LogContext.PushProperty("userId", userId);
            await next();
        });
    }
}