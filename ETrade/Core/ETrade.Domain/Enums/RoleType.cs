using System.ComponentModel;

namespace ETrade.Domain.Enums;

public enum RoleType
{
    [Description("Sahibi")]
    Owner = 1,
    
    [Description("Admin")]
    Admin = 2,
    
    [Description("Yönetici")]
    Manager = 3,
    
    [Description("Editör")]
    Editor = 4,
    
    [Description("Kullanıcı")]
    User = 5,
    
    [Description("Müşteri")]
    Buyer = 6,
    
    [Description("Satıcı")]
    Seller = 7
}