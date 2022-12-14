using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.Base;
using ETrade.Domain.Enums;

namespace ETrade.Application.Features.Accounts.DTOs;


public class UserSummaryCardDto:BaseDto
    {
        public string Id { get; set; }

        [Display(Name = "Ad")]
        public string FirstName { get; set; }
        
        [Display(Name = "Soyad")]
        public string LastName { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Cinsiyet")]
        public GenderType GenderType { get; set; }
        
        [Display(Name = "Varsayılan Adres")]
        public string DefaultAddressDetail { get; set; }
        
    }