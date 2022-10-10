using System.ComponentModel;

namespace ETrade.Domain.Enums;
public enum ModelColourType
{
    [Description("Altın")]
    Gold = 1,
    
    [Description("Bej")]
    Beige = 2,
    
    [Description("Beyaz")]
    White = 3,
    
    [Description("Bordo")]
    Burgundy = 4,
    
    [Description("Füme")]
    Smoked = 5,
    
    [Description("Gri")]
    Grey = 6,

    [Description("Gümüş Gri")]
    SilverGrey = 7,
    
    [Description("Kahverengi")]
    Brown = 8,
    
    [Description("Kırmızı")]
    Red = 9,
    
    [Description("Lacivert")]
    NavyBlue = 10,
    
    [Description("Mavi")]
    Blue = 11,

    [Description("Pembe")]
    Pink = 12,
    
    [Description("Sarı")]
    Yellow = 13,
    
    [Description("Siyah")]
    Black = 14,
    
    [Description("Şampanya")]
    Champagne = 15,
    
    [Description("Turkuaz")]
    Turquoise = 16,
    
    [Description("Turuncu")]
    Orange = 17,
    
    [Description("Yeşil")]
    Green = 18,

    [Description("Diğer")]
    Other = 19
}