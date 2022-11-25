using System.ComponentModel.DataAnnotations;
using ETrade.Domain.Entities.Common;
using ETrade.Domain.Enums;

namespace ETrade.Domain.Entities;

public class IpAddress:BaseEntity,IEntity
    {
        public int Id { get; set; }
        public string RangeStart { get; set; }
        public string RangeEnd { get; set; }
        public  IpListType IpListType { get; set; }
        
        public  List<Endpoint> Endpoints{ get; set; }
    }
