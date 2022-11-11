using ETrade.Domain.Entities.Common;

namespace ETrade.Domain.Entities;

public class Menu:BaseEntity,IEntity
{
    public string ControllerName { get; set; }
    public string AreaName { get; set; }
    public string Definition { get; set; }
    public List<Action> Actions { get; set; } = new List<Action>();
}