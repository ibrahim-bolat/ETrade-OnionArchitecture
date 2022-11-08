using ETrade.Domain.Entities.Common;

namespace ETrade.Domain.Entities;

public class Menu:BaseEntity,IEntity
{
    public string Name { get; set; }
    public bool Checked { get; set; }
    public List<Action> Actions { get; set; } = new List<Action>();
}