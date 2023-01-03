using System.Net;
using System.Threading.RateLimiting;
using ETrade.Application.Model;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;

namespace ETrade.MVC.Configurations.RateLimit;

public class CustomRateLimitPolicy : IRateLimiterPolicy<string>
{
    private readonly RateLimitSettings _rateLimitSettings;
    private Func<OnRejectedContext, CancellationToken, ValueTask> _onRejected;
    public CustomRateLimitPolicy(IOptions<RateLimitSettings> options)
    {
        _rateLimitSettings = options.Value;
        _onRejected = (onRejectedContext, token) =>
        {
            int statusCode = StatusCodes.Status429TooManyRequests;
            string errorTitle=WebUtility.UrlEncode("İstek Limiti Aşılmıştır.");
            string errorMessage;
            if (onRejectedContext.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
            {
                errorMessage=WebUtility.UrlEncode($"İstek Limit Kotası Aşıldı. {_rateLimitSettings.Window} saniyede {_rateLimitSettings.PermitLimit} istek yapabilirsiniz. Lütfen {retryAfter.Seconds} saniye sonra tekrar deneyiniz.");
                
            }
            else
            {
                errorMessage=WebUtility.UrlEncode($"İstek Limit Kotası Aşıldı. {_rateLimitSettings.Window} saniyede {_rateLimitSettings.PermitLimit} istek yapabilirsiniz. Lütfen çok sık aralıklarla istek yapmayınız!.");
            }
            onRejectedContext.HttpContext.Response.Redirect($"/Error/Index?statusCode={statusCode}&errorTitle={errorTitle}&errorMessage={errorMessage}");
            return ValueTask.CompletedTask;
        };
    }
    public Func<OnRejectedContext, CancellationToken, ValueTask> OnRejected => _onRejected;
    
    public RateLimitPartition<string> GetPartition(HttpContext httpContext)
    {
        return RateLimitPartition.GetFixedWindowLimiter(string.Empty, _ =>
        new FixedWindowRateLimiterOptions{
           PermitLimit = _rateLimitSettings.PermitLimit,
           QueueLimit = _rateLimitSettings.QueueLimit,
           Window = TimeSpan.FromSeconds(_rateLimitSettings.Window),
           QueueProcessingOrder = _rateLimitSettings.QueueProcessingOrder,
           AutoReplenishment = _rateLimitSettings.AutoReplenishment
        });
    }
}