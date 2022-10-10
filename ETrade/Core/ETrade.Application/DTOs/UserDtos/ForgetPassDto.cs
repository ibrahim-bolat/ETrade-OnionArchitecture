using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.Common;

namespace ETrade.Application.DTOs.UserDtos;


public class ForgetPassDto:BaseDto,IDto
    {
        [Display(Name = "E-Posta Adresiniz")]
        public string Email { get; set; }
    }
