using System.ComponentModel;

namespace ETrade.Domain.Enums;

public enum BodyType
{
    [Description("Cabrio")]
    Cabrio = 1,
    
    [Description("Coupe")]
    Coupe = 2,
    
    [Description("Hatchback 3 Kapı")]
    Hatchback3Door = 3,
    
    [Description("Hatchback 5 Kapı")]
    Hatchback5Door = 4,
    
    [Description("Sedan")]
    Sedan = 5,
    
    [Description("Station Wagon")]
    StationWagon = 6,
    
    [Description("MPV")]
    Mpv = 7,
    
    [Description("Roodster")]
    Roodster = 8
}