using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.Common;

namespace ETrade.Application.Features.UserOperations.DTOs;

public class RoleDto:BaseDto,IDto
    {
        [Display(Name = "Rol Adı")]
        public string Name { get; set; }
    }
