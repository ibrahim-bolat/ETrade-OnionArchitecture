
using ETrade.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Domain.Entities.Identity;

public class AppRole:IdentityRole<int>,IEntity
    {
        public  DateTime CreatedTime { get; set; } = DateTime.Now;
        public  DateTime ModifiedTime { get; set; } = DateTime.Now;
        public  bool IsActive { get; set; } = true;
        public  bool IsDeleted { get; set; } = false;
        public  string CreatedByName { get; set; } = "Owner";
        public  string ModifiedByName { get; set; } = "Owner";
        public  string Note { get; set; }
        
        public  List<Endpoint> Endpoints{ get; set; }
    }