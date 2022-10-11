using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.Common;

namespace ETrade.Application.Features.Accounts.DTOs.RoleDtos;

public class RoleDto:BaseDto,IDto
    {
        [Display(Name = "Rol Adı")]
        public string Name { get; set; }
    }
