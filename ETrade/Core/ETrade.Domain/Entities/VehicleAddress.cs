using ETrade.Domain.Entities.Common;

namespace ETrade.Domain.Entities;

    public class VehicleAddress : BaseEntity
    {
        public  string AddressTitle{ get; set; }
        public  string NeighborhoodOrVillage{ get; set; }
        public  string District{ get; set; }
        public  string City{ get; set; }
        public  string PostalCode{ get; set; }
        public  string AddressDetails{ get; set; }
        
        public Ad Ad { get; set; }
    }
