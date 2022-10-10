using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.Common;

namespace ETrade.Application.DTOs.UserDtos;


public class LoginDto:BaseDto,IDto
    {
        [Display(Name = "E-Posta ")]
        public string Email { get; set; }
        

        [Display(Name = "Şifre")]
        public string Password { get; set; }
        

        [Display(Name = "Beni Hatırla")]
        public bool Persistent { get; set; }
        
        public bool Lock { get; set; }
    }
