using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.Base;

namespace ETrade.Application.DTOs.Common;

public class RoleDto:BaseDto,IDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Rol Adı")]
        public string Name { get; set; }
    }
