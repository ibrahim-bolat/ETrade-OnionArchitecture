using System.Threading.RateLimiting;

namespace  ETrade.Application.Model;

public class RateLimitSettings
{
    public int PermitLimit { get; set; } = 8;
    public int QueueLimit { get; set; } = 2;
    public int Window { get; set; } = 1;
    public QueueProcessingOrder QueueProcessingOrder { get; set; } = QueueProcessingOrder.OldestFirst;
    public bool AutoReplenishment { get; set; } = true;
}