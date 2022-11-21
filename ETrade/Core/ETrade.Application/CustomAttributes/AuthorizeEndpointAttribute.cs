using ETrade.Domain.Enums;

namespace ETrade.Application.CustomAttributes;

public class AuthorizeEndpointAttribute:Attribute
{
    public string Menu { get; set; }
    public string Definition { get; set; }
    public ActionType ActionType { get; set; }
}