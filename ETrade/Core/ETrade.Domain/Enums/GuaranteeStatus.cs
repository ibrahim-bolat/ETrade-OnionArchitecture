using System.ComponentModel;

namespace ETrade.Domain.Enums;

public enum GuaranteeStatus
{
    [Description("Evet")]
    Yes = 1,
    
    [Description("Hayır")]
    No = 2
}