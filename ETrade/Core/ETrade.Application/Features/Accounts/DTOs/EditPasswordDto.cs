using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.Common;

namespace ETrade.Application.Features.Accounts.DTOs;

public class EditPasswordDto:BaseDto,IDto
        {
            public int Id { get; set; }
            
            [Display(Name = "Kullanıcı Adı")]
            public string UserName { get; set; }

            [Display(Name = "Yeni Şifre")]
            public string NewPassword { get; set; }

            [Display(Name = "Yeni Şifre Tekrar")]
            public string ReNewPassword { get; set; }
        }

