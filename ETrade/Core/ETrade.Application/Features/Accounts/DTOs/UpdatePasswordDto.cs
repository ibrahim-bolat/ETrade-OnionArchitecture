using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.Base;

namespace ETrade.Application.Features.Accounts.DTOs;


public class UpdatePasswordDto:BaseDto
    {
        [Display(Name = "Yeni Şifre")]
        public string Password { get; set; }
        
        [Display(Name = "Yeni Şifre Tekrar")]
        public string RePassword { get; set; }
    }
