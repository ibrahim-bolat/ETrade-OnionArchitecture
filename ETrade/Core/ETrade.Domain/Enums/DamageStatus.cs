using System.ComponentModel;

namespace ETrade.Domain.Enums;

public enum DamageStatus
{
    [Description("Ağır Hasarlı")]
    HeavilyDamaged = 1,
    
    [Description("Belirtilmemiş")]
    Unspecified = 2,
    
    [Description("Boyasız ve Değişensiz")]
    UnpaintedAndUnchanged = 3,
    
    [Description("Değişensiz")]
    Unchanging = 4,
    
    [Description("Tramersiz")]
    WithoutDamageRegistration = 5
}