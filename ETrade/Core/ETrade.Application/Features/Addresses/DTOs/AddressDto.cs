using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.Base;
using ETrade.Domain.Enums;

namespace ETrade.Application.Features.Addresses.DTOs;

public class AddressDto:BaseDto,IDto
{
    public int Id { get; set; }
    
    [Display(Name = "Not")]
    public  string Note { get; set; }
    
    [Display(Name = "Adı")]
    public string FirstName { get; set; }
    
    [Display(Name = "Soyadı")]
    public string LastName { get; set; }
    
    [Display(Name = "Email")]
    public string Email{ get; set; }
    
    [Display(Name = "Telefon")]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber{ get; set; }
    
    [Display(Name = "Adres Başlığı")]
    public  string AddressTitle{ get; set; }
    
    [Display(Name = "Adres Tipi")]
    public  AddressType AddressType { get; set; }

    [Display(Name = "Mahalle ya da Köy")]
    public  string NeighborhoodOrVillage{ get; set; }
    
    [Display(Name = "İlçe")]
    public  string District{ get; set; }
    
    [Display(Name = "İl")]
    public  string City{ get; set; }

    [Display(Name = "Posta Kodu")]
    public  string PostalCode{ get; set; }
    
    [Display(Name = "Detaylı Adres")]
    public  string AddressDetails{ get; set; }
    
    [Display(Name = "Varsayılan Adresmi?")]
    public bool DefaultAddress { get; set; }
    
    public int UserId { get; set; }
}