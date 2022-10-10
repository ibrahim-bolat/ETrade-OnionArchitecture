using ETrade.Domain.Entities.Common;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;

namespace ETrade.Domain.Entities;

    public class Address : BaseEntity,IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email{ get; set; }
        public string PhoneNumber{ get; set; }
        public  string AddressTitle{ get; set; }
        public  AddressType AddressType { get; set; }
        public  string NeighborhoodOrVillage{ get; set; }
        public  string District{ get; set; }
        public  string City{ get; set; }
        public  string PostalCode{ get; set; }
        public  string AddressDetails{ get; set; }
        public bool DefaultAddress { get; set; }
        public int UserId{ get; set; }
        public AppUser AppUser{ get; set; }
    
}
