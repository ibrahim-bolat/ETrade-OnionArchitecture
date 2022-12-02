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
        int statusCode = (int)HttpStatusCode.TooManyRequests;
        string errorTitle=WebUtility.UrlEncode("İstek Limiti Aşılmıştır.");
        string errorMessage=WebUtility.UrlEncode($"İstek Limit Kotası Aşıldı. {rule.Period} başına {rule.Limit} istek yapabilirsiniz. Lütfen {retryAfter} saniye sonra tekrar deneyiniz.");
        httpContext.Response.Redirect($"/ErrorInfo/QuotaExceededRateLimit?statusCode={statusCode}&errorTitle={errorTitle}&errorMessage={errorMessage}");
        return Task.CompletedTask;
    }
}