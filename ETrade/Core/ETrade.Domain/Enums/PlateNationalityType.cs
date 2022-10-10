using System.ComponentModel;

namespace ETrade.Domain.Enums;

public enum PlateNationalityType
{
    [Description("Türkiye (TR) Plakalı")]
    TurkeyPlate = 1,
    
    [Description("Yabancı Plakalı")]
    ForeignPlate = 2,
    
    [Description("Mavi (MA) Plakalı")]
    BluePlate = 3,
}