using AutoMapper;
using ETrade.Application.Constants;
using ETrade.Application.DTOs.AddressDtos;
using ETrade.Application.Services.Abstract;
using ETrade.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class AddressController : Controller
{
    private readonly IAddressService _addressService;
    private readonly IMapper _mapper;

    public AddressController(IAddressService addressService, IMapper mapper)
    {
        _addressService = addressService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult AddressAdd(int userId)
    {
        AddressDto addressDto = new AddressDto();
        addressDto.UserId = userId;
        return View(addressDto);
    }

    [HttpPost]
    public async Task<IActionResult> AddressAdd(AddressDto addressDto)
    {
        if (ModelState.IsValid)
        {
            var dresult= await _addressService.AddAsync(addressDto, User.Identity?.Name);
            if (dresult.Message == Messages.AddressCountMoreThan10)
            {
                ModelState.AddModelError("AddressCountMoreThan10", Messages.AddressCountMoreThan10);
                return View(addressDto);
            }
            if (dresult.ResultStatus == ResultStatus.Success)
            {
                TempData["AddAddressSuccess"] = true;
                return RedirectToAction("AddressAdd", "Address" ,new  {userId=addressDto.UserId});
            }
        }
        return View(addressDto);
    }

    [HttpGet]
    public async Task<IActionResult> AddressUpdate(int addressId)
    {
        if (addressId > 0)
        {
            var dresult = await _addressService.GetAsync(addressId);
            if (dresult.ResultStatus==ResultStatus.Success)
            {
                AddressDto addressDto = _mapper.Map<AddressDto>(dresult.Data);
                return View(addressDto);
            }
            
        }
        return RedirectToAction("AllErrorPages", "ErrorPages", new { statusCode = 404 });
    }

    [HttpPost]
    public async Task<IActionResult> AddressUpdate(AddressDto addressDto)
    {
        if (ModelState.IsValid)
        {
            var dresult= await _addressService.UpdateAsync(addressDto, User.Identity?.Name);
            if (dresult.ResultStatus == ResultStatus.Success)
            {
                TempData["UpdateAddressSuccess"] = true;
                return RedirectToAction("AddressUpdate", "Address" ,new { addressId=addressDto.Id});
            }
        }
        return View(addressDto);
    }

    [HttpGet]
    public async Task<IActionResult>  AddressDetail(int addressId)
    {
        if (addressId > 0)
        {
            var dresult = await _addressService.GetAsync(addressId);
            if (dresult.ResultStatus==ResultStatus.Success)
            {
                AddressDto addressDto = _mapper.Map<AddressDto>(dresult.Data);
                return View(addressDto);
            }
        }
        return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
    }
    
    [HttpPost]
    public async Task<IActionResult> AddressDelete(int addressId)
    {
        if (addressId > 0)
        {
            var dresult = await _addressService.DeleteAsync(addressId,User.Identity?.Name);
            if (dresult.ResultStatus==ResultStatus.Success)
            {
                var userId = dresult.Data.UserId;
                return Json(new { success = true, userId });
            }
            return Json(new { success = false});
        }
        return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
    }
}