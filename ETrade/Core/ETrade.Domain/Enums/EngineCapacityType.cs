using System.ComponentModel;

namespace ETrade.Domain.Enums;

public enum EngineCapacityType
{
    [Description("1300 cm3 ve Altı")]
    Cm01300 = 1,
    
    [Description("1301 - 1600 cm3")]
    Cm13011600 = 2,
    
    [Description("1601 - 1800 cm3")]
    Cm16011800 = 3,
    
    [Description("1801 - 2000 cm3")]
    Cm18012000 = 4,
    
    [Description("2001 - 2500 cm3")]
    Cm20012500 = 5,
    
    [Description("2501 - 3000 cm3")]
    Cm25013000 = 6,

    [Description("3001 - 3500 cm3")]
    Cm30013500 = 7,
    
    [Description("3501 - 4000 cm3")]
    Cm35014000 = 8,
    
    [Description("4001 - 4500 HP")]
    Hp40014500 = 9,
    
    [Description("4501 - 5000 cm3")]
    Cm45015000 = 10,
    
    [Description("5001 - 5500 HP")]
    Hp50015500 = 11,

    [Description("5501 - 6000 cm3")]
    Cm55016000 = 12,
    
    [Description("6001 cm3 ve Üstü")]
    Hp6001 = 13
}