using System.ComponentModel;

namespace ETrade.Domain.Enums;

public enum GenderType
{
    [Description("Belirtilmemiş")]
    Unspecified,
    
    [Description("Erkek")]
    Male,
    
    [Description("Kadın")]
    Female,
}