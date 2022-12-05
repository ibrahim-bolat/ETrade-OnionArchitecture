using ETrade.Domain.Entities.Common;
using ETrade.Domain.Entities.Identity;

namespace ETrade.Domain.Entities;

public class RequestInfoLog:BaseEntity
{
    public string AreaName { get; set; }
    
    public string ControllerName { get; set; }
    
    public string ActionName { get; set; }
    public string RequestMethodType { get; set; }

    public DateTime DateTime { get; set; }
    public List<string> ActionArguments { get; set; }
    
    public string LocalIpAddress { get; set; }
    public string RemoteIpAddress { get; set; }
    public int LocalPort { get; set; }
    public int RemotePort { get; set; }
    public int? UserId{ get; set; }
    
    public AppUser AppUser { get; set; }
}