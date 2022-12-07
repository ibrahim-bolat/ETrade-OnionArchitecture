using System.ComponentModel.DataAnnotations.Schema;
using ETrade.Domain.Entities.Common;


namespace ETrade.Domain.Entities;

public class City : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<District> Districts{ get; set; }
}
