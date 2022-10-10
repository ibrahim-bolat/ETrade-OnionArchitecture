
using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.Common;

namespace ETrade.Application.DTOs.UserImageDtos;

public class UserImageDto:BaseDto,IDto
{
    public int Id { get; set; }
    
    [Display(Name = "Not")]
    public string Note { get; set; }
    
    [Display(Name = "Resim Başlığı")]
    public string ImageTitle { get; set; }
    
    [Display(Name = "Resim Yolu")]
    public string ImagePath { get; set; }
    
    [Display(Name = "Resim Kısa Açıklmaa")]
    public string ImageAltText { get; set; }
    
    [Display(Name = "Profil Resmimi?")]
    public bool Profil { get; set; }
    
    public int UserId { get; set; }
}