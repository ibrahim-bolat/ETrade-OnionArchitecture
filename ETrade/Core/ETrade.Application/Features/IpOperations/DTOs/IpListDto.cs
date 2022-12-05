using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.Base;
using ETrade.Domain.Enums;

namespace ETrade.Application.Features.IpOperations.DTOs;

public class IpListDto:BaseDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Ip Aralık Başlangıcı")]
        public string RangeStart { get; set; }
        
        [Display(Name = "Ip Aralık Sonu")]
        public string RangeEnd { get; set; }
        
        [Display(Name = "Ip Liste Tipi")]
        public  string IpListType { get; set; }
        
        [Display(Name = "Durumu")]
        public bool Status { get; set; }

    }
