using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.Base;

namespace ETrade.Application.Features.Accounts.DTOs;

public class ForgetPassDto:BaseDto,IDto
    {
        [Display(Name = "E-Posta Adresiniz")]
        public string Email { get; set; }
    }