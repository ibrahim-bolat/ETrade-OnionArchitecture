using AutoMapper;
using ETrade.Application.Features.Addresses.Commands.CreateAddressCommand;
using ETrade.Application.Features.Addresses.Commands.DeleteAddressCommand;
using ETrade.Application.Features.Addresses.Commands.UpdateAddressCommand;
using ETrade.Application.Features.Addresses.Constants;
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Features.Addresses.Queries.GetByIdAddressQuery;
using ETrade.Application.Services.Abstract;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class AddressController : Controller
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AddressController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
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
            var dresult = await _mediator.Send(new CreateAddressCommandRequest
            {
                AddressDto = addressDto, 
                CreatedByName = User.Identity?.Name
            });
            if (dresult.Result.Message == Messages.AddressCountMoreThan4)
            {
                ModelState.AddModelError("AddressCountMoreThan10", Messages.AddressCountMoreThan4);
                return View(addressDto);
            }
            if (dresult.Result.ResultStatus == ResultStatus.Success)
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
            var dresult = await _mediator.Send(new GetByIdAddressQueryRequest
            {
                Id = addressId
            });
            if (dresult.Result.ResultStatus==ResultStatus.Success)
            {
                AddressDto addressDto = _mapper.Map<AddressDto>(dresult.Result.Data);
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
            var dresult = await _mediator.Send(new UpdateAddressCommandRequest
            {
                AddressDto = addressDto, 
                ModifiedByName = User.Identity?.Name
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
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
            var dresult = await _mediator.Send(new GetByIdAddressQueryRequest
            {
                Id = addressId
            });
            if (dresult.Result.ResultStatus==ResultStatus.Success)
            {
                AddressDto addressDto = _mapper.Map<AddressDto>(dresult.Result.Data);
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
            var dresult = await _mediator.Send(new DeleteAddressCommandRequest
            {
                Id = addressId, 
                ModifiedByName = User.Identity?.Name
            });
            if (dresult.Result.ResultStatus==ResultStatus.Success)
            {
                var userId = dresult.Result.Data.UserId;
                return Json(new { success = true, userId });
            }
            return Json(new { success = false});
        }
        return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
    }
}