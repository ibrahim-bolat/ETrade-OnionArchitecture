using System.ComponentModel;

namespace ETrade.Domain.Enums;

public enum TractionType
{
    [Description("Önden Çekiş")]
    FrontDrive = 1,
    
    [Description("Arkadan Çekiş")]
    RearDrive = 2,
    
    [Description("4WD (Sürekli)")]
    FourWdContinuous = 3,
    
    [Description("AWD (Elektronik)")]
    AwdElectronic = 4
}