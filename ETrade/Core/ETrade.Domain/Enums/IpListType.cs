using System.ComponentModel;

namespace ETrade.Domain.Enums;
public enum IpListType
    {
        [Description("Beyaz Liste")]
        WhiteList = 1,
        
        [Description("Kara Liste")]
        BlackList = 2,
    }
