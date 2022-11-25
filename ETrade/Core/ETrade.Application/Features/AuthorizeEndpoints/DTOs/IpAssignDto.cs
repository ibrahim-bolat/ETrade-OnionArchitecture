using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.Base;
using ETrade.Domain.Enums;

namespace ETrade.Application.Features.AuthorizeEndpoints.DTOs;


public class IpAssignDto:BaseDto,IDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Ip Aralık Başlangıcı")]
        public string RangeStart { get; set; }
        
        [Display(Name = "Ip Aralık Sonu")]
        public string RangeEnd { get; set; }
        
        [Display(Name = "Ip Liste Tipi")]
        public  string IpListType { get; set; }
        
        public bool HasAssign { get; set; }
    }
