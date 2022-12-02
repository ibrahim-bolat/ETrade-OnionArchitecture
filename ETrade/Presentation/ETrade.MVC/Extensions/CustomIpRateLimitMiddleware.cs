using System.Net;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ETrade.MVC.Extensions;

public class CustomIpRateLimitMiddleware:IpRateLimitMiddleware
{
    public CustomIpRateLimitMiddleware(RequestDelegate next, IProcessingStrategy processingStrategy, IOptions<IpRateLimitOptions> options, IIpPolicyStore policyStore, IRateLimitConfiguration config, ILogger<IpRateLimitMiddleware> logger) : base(next, processingStrategy, options, policyStore, config, logger)
    {
    }

    public override Task ReturnQuotaExceededResponse(HttpContext httpContext, RateLimitRule rule, string retryAfter)
    {
        httpContext.Response.Headers["Retry-After"] = retryAfter;
        int statusCode = (int)HttpStatusCode.TooManyRequests;
        httpContext.Response.Redirect($"/ErrorPages/QuotaExceededRateLimit?limit={rule.Limit}&period={rule.Period}&retryAfter={retryAfter}&errorNumber={statusCode}");
        return Task.CompletedTask;
    }
}