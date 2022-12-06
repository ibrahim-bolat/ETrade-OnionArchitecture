using System.ComponentModel.DataAnnotations.Schema;
using ETrade.Domain.Entities.Common;

namespace ETrade.Domain.Entities;

public class NeighborhoodOrVillage : BaseEntity
{
    public string Name { get; set; }
    public int DistrictId { get; set; }
    public District District { get; set; }
    public List<Street> Streets { get; set; }
}