using ETrade.Domain.Entities.Common;
using ETrade.Domain.Entities.Identity;

namespace ETrade.Domain.Entities;

public class Action:BaseEntity,IEntity
{
    public string ActionName { get; set; }
    
    public string ControllerName { get; set; }
    
    public string AreaName { get; set; }
    public string ActionType { get; set; }
    public string HttpType { get; set; }
    public string Definition { get; set; }
    public string Code { get; set; }
    
    public int MenuId{ get; set; }
    public Menu Menu { get; set; }
    
    public  List<AppRole> AppRoles{ get; set; }
}