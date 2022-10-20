using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.Common;

namespace ETrade.Application.Features.RoleOperations.DTOs;



public class RoleAssignDto:BaseDto,IDto
    {
        public int Id { get; set; }
        
        
        [Display(Name = "Rol Adı")]
        public string Name { get; set; }
        public bool HasAssign { get; set; }
    }
