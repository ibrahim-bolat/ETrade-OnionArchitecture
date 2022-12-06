using System.ComponentModel.DataAnnotations.Schema;
using ETrade.Domain.Entities.Common;


namespace ETrade.Domain.Entities;

public class City : BaseEntity
{
    public string Name { get; set; }
    public List<District> Districts{ get; set; }
}
