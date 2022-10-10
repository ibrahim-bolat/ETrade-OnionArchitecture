using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.Application.DTOs.RoleDtos;


public class RoleOperationDto:BaseDto,IDto
    {
        
        [Display(Name = "Id")]
        [HiddenInput]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Rolü boş geçmeyiniz.")]
        [Display(Name = "Rol Adı")]
        public string Name { get; set; }
        
        
        public bool HasAssign { get; set; }
    }