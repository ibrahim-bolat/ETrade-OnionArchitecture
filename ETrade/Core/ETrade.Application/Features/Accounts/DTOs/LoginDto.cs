using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.Base;

namespace ETrade.Application.Features.Accounts.DTOs;


public class LoginDto:BaseDto
    {
        [Display(Name = "E-Posta ")]
        public string Email { get; set; }
        

        [Display(Name = "Şifre")]
        public string Password { get; set; }
        

        [Display(Name = "Beni Hatırla")]
        public bool Persistent { get; set; }
        
        public bool Lock { get; set; }
    }
