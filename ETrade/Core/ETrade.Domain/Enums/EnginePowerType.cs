using System.ComponentModel;

namespace ETrade.Domain.Enums;

public enum EnginePowerType
{
    [Description("50 HP ve Altı")]
    Hp050 = 1,
    
    [Description("51 - 75 HP")]
    Hp5175 = 2,
    
    [Description("76 - 100 HP")]
    Hp76100 = 3,
    
    [Description("101 - 125 HP")]
    Hp101125 = 4,
    
    [Description("126 - 150 HP")]
    Hp126150 = 5,
    
    [Description("151 - 175 HP")]
    Hp151175 = 6,

    [Description("176 - 200 HP")]
    Hp176200 = 7,
    
    [Description("201 - 225 HP")]
    Hp201225 = 8,
    
    [Description("226 - 250 HP")]
    Hp226250 = 9,
    
    [Description("251 - 275 HP")]
    Hp251275 = 10,
    
    [Description("276 - 300 HP")]
    Hp276300 = 11,

    [Description("301 - 325 HP")]
    Hp301325 = 12,
    
    [Description("326 - 350 HP")]
    Hp326350 = 13,
    
    [Description("351 - 375 HP")]
    Hp351375 = 14,
    
    [Description("376 - 400 HP")]
    Hp376400 = 15,
    
    [Description("401 - 425 HP")]
    Hp401425 = 16,
    
    [Description("426 - 450 HP")]
    Hp426450 = 17,
    
    [Description("451 - 475 HP")]
    Hp451475 = 18,

    [Description("476 - 500 HP")]
    Hp476500 = 19,
    
    [Description("501 - 525 HP")]
    Hp501525 = 15,
    
    [Description("526 - 550 HP")]
    Hp526550 = 16,
    
    [Description("551 - 575 HP")]
    Hp551575 = 17,
    
    [Description("576 - 600 HP")]
    Hp576600 = 18,

    [Description("601 HP ve Üzeri")]
    Hp601 = 19
}