using ETrade.Application.DTOs.Common;

namespace ETrade.Application.DTOs.RoleDtos;



public class RoleAssignDto:BaseDto,IDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool HasAssign { get; set; }
    }
