using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.Base;
using ETrade.Application.Features.UserImages.Validations.CustomValidations;
using Microsoft.AspNetCore.Http;

namespace ETrade.Application.Features.UserImages.DTOs;

public class CreateUserImageDto:BaseDto
{
    public int Id { get; set; }
    
    [Display(Name = "Not")]
    public string Note { get; set; }
    
    [Display(Name = "Resim Başlığı")]
    public string ImageTitle { get; set; }
    
    
    [Display(Name = "Resim Kısa Açıklmaa")]
    public string ImageAltText { get; set; }
    
    [Display(Name = "Profil Resmimi?")]
    public bool Profil { get; set; }
    
    [Display(Name="Resim Yükle")]
    [DataType(DataType.Upload)]
    [ImageValidation(extensions:new string[] { ".jpg",".jpeg" , ".png" },maxFileSize:(5* 1024 * 1024),minWidth:197,minHeight:150)]
    //image in uzantıları,maxsimum dosya boyutu,minumum genişliği ve yüksekliği ile ilgili custom validation
    public IFormFile ImageFile { get; set; }
    
    public int UserId { get; set; }
}