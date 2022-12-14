using System.ComponentModel.DataAnnotations.Schema;
using ETrade.Domain.Entities.Common;


namespace ETrade.Domain.Entities;

public class District : IEntity
{
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public List<NeighborhoodOrVillage> NeighborhoodsOrVillages{ get; set; }
}
