using ETrade.Domain.Entities.Common;
using ETrade.Domain.Entities.Identity;

namespace ETrade.Domain.Entities;

public class Action:BaseEntity,IEntity
{
    public string ActionType { get; set; }
    public string HttpType { get; set; }
    public string Definition { get; set; }
    public string Code { get; set; }
    
    public bool Checked { get; set; }
    public int MenuId{ get; set; }
    public Menu Menu { get; set; }
    
    public  List<AppRole> AppRoles{ get; set; }
}