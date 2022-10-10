using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.Common;

namespace ETrade.Application.DTOs.UserDtos;


public class UpdatePasswordDto:BaseDto,IDto
    {
        [Display(Name = "Yeni Şifre")]
        public string Password { get; set; }
        
        [Display(Name = "Yeni Şifre Tekrar")]
        public string RePassword { get; set; }
    }
