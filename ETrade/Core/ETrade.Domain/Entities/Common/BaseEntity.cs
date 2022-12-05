using ETrade.Domain.Enums;

namespace ETrade.Domain.Entities.Common;

public abstract class BaseEntity:IEntity
{
    public virtual int Id { get; set; }
    public virtual DateTime CreatedTime { get; set; } = DateTime.Now;
    public virtual DateTime ModifiedTime { get; set; } = DateTime.Now;
    public virtual bool IsActive { get; set; } = true;
    public virtual bool IsDeleted { get; set; } = false;
    public virtual string CreatedByName { get; set; } = RoleType.Admin.ToString();
    public virtual string ModifiedByName { get; set; } = RoleType.Admin.ToString();
    public virtual string Note { get; set; }
}