namespace ETrade.Domain.Entities.Common;

public interface IIdentityEntity:IEntity
{
    public DateTime CreatedTime { get; set; } 
    public DateTime ModifiedTime { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public string CreatedByName { get; set; }
    public string ModifiedByName { get; set; }
    public string Note { get; set; }
}