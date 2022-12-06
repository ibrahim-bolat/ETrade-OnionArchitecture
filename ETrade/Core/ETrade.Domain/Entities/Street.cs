using System.ComponentModel.DataAnnotations.Schema;
using ETrade.Domain.Entities.Common;

namespace ETrade.Domain.Entities;

public class Street : BaseEntity
{
    public string Name { get; set; }
    public int NeighborhoodOrVillageId { get; set; }
    public NeighborhoodOrVillage NeighborhoodOrVillage { get; set; }
}