using ETrade.Application.Constants;
using ETrade.Application.CustomAttributes;
using ETrade.Application.Features.Addresses.Commands.CreateAddressCommand;
using ETrade.Application.Features.Addresses.Commands.DeleteAddressCommand;
using ETrade.Application.Features.Addresses.Commands.UpdateAddressCommand;
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Features.Addresses.Queries.GetByIdAddressQuery;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Messages = ETrade.Application.Features.Addresses.Constants.Messages;

namespace ETrade.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class AddressController : Controller
{
    private readonly IMediator _mediator;

    public AddressController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.Address, ActionType = ActionType.Reading, Definition = "Get Address Index Page")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.Address, ActionType = ActionType.Reading, Definition = "Get By Id Address for Create Address")]
    public IActionResult CreateAddress(int userId)
    {
        AddressDto addressDto = new AddressDto();
        addressDto.UserId = userId;
        return View(addressDto);
    }

    [HttpPost]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.Address, ActionType = ActionType.Writing, Definition = "Create Address")]
    public async Task<IActionResult> CreateAddress(AddressDto addressDto)
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
                ModelState.AddModelError("AddressCountMoreThan4", Messages.AddressCountMoreThan4);
                return View(addressDto);
            }
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                TempData["AddAddressSuccess"] = true;
                return RedirectToAction("CreateAddress", "Address" ,new  {userId=addressDto.UserId});
            }
        }
        return View(addressDto);
    }

    [HttpGet]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.Address, ActionType = ActionType.Reading, Definition = "Get By Id Address for Update Address")]
    public async Task<IActionResult> UpdateAddress(int addressId)
    {
        if (addressId > 0)
        {
            var dresult = await _mediator.Send(new GetByIdAddressQueryRequest
            {
                Id = addressId
            });
            if (dresult.Result.ResultStatus==ResultStatus.Success)
            {
                return View(dresult.Result.Data);
            }
            
        }
        return RedirectToAction("AllErrorPages", "ErrorPages", new { statusCode = 404 });
    }

    [HttpPost]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.Address, ActionType = ActionType.Updating, Definition = "Update Address")]
    public async Task<IActionResult> UpdateAddress(AddressDto addressDto)
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
                return RedirectToAction("UpdateAddress", "Address" ,new { addressId=addressDto.Id});
            }
        }
        return View(addressDto);
    }

    [HttpGet]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.Address, ActionType = ActionType.Reading, Definition = "Get By Id Address for Address Details")]
    public async Task<IActionResult>  DetailAddress(int addressId)
    {
        if (addressId > 0)
        {
            var dresult = await _mediator.Send(new GetByIdAddressQueryRequest
            {
                Id = addressId
            });
            if (dresult.Result.ResultStatus==ResultStatus.Success)
            {
                return View(dresult.Result.Data);
            }
        }
        return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
    }
    
    [HttpPost]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.Address, ActionType = ActionType.Deleting, Definition = "Delete Address")]
    public async Task<IActionResult> DeleteAddress(int addressId)
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