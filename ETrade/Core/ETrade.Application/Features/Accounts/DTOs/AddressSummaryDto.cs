using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.Base;

namespace ETrade.Application.Features.Accounts.DTOs;
public class AddressSummaryDto:BaseDto
{
    public int Id { get; set; }
    
    [Display(Name = "Ad Soyad")]
    public  string FullName{ get; set; }
    
    [Display(Name = "Telefon")]
    public string PhoneNumber { get; set; }
    
    [Display(Name = "Adres Başlığı")]
    public  string AddressTitle{ get; set; }
    
    [Display(Name = "Detaylı Adres")]
    public  string AddressDetails{ get; set; }
    
    [Display(Name = "Varsayılan Adres")]
    public bool DefaultAddress { get; set; }
}