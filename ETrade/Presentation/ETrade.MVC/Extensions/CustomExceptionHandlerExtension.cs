using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Diagnostics;

namespace ETrade.MVC.Extensions;

public static class CustomExceptionHandlerExtension
{
    public static void CustomExceptionHandler(this WebApplication app)
    {
        ILogger<Program> logger = app.Services.GetRequiredService<ILogger<Program>>();
        app.UseExceptionHandler(builder =>
        {
            builder.Run(context =>
            {
                var statusCode = (int)HttpStatusCode.InternalServerError;

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    logger.LogError(contextFeature.Error.Message);
                    context.Response.Redirect($"/ErrorPages/AllErrorPages?statusCode={statusCode}");
                }
                return Task.CompletedTask;
            });
        });
    }
}