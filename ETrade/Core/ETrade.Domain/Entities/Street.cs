using System.ComponentModel.DataAnnotations.Schema;
using ETrade.Domain.Entities.Common;

namespace ETrade.Domain.Entities;

public class Street : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int NeighborhoodOrVillageId { get; set; }
    public NeighborhoodOrVillage NeighborhoodOrVillage { get; set; }
}