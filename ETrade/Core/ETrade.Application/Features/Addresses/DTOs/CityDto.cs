using System.ComponentModel.DataAnnotations;
using ETrade.Application.DTOs.Base;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ETrade.Application.Features.Addresses.DTOs;

public class CityDto:BaseDto
{
    public int Id { get; set; }
    
    [Display(Name = "İl Adı")]
    public string Name { get; set; }
    
    [Display(Name = "İller")]
    public  List<SelectListItem> Cities { get; set; }

}