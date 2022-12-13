using ETrade.Application.Constants;
using ETrade.Application.CustomAttributes;
using ETrade.Application.Features.Addresses.Commands.CreateAddressCommand;
using ETrade.Application.Features.Addresses.Commands.DeleteAddressCommand;
using ETrade.Application.Features.Addresses.Commands.UpdateAddressCommand;
using ETrade.Application.Features.Addresses.DTOs;
using ETrade.Application.Features.Addresses.Queries.GetByIdDetailAddressQuery;
using ETrade.Application.Features.Addresses.Queries.GetByIdUpdateAddressQuery;
using ETrade.Application.Features.Addresses.Queries.GetCreateAddressQuery;
using ETrade.Application.Features.Addresses.Queries.GetDistrictListQuery;
using ETrade.Application.Features.Addresses.Queries.GetNeighborhoodOrVillageListQuery;
using ETrade.Application.Features.Addresses.Queries.GetSelectedAddressQuery;
using ETrade.Application.Features.Addresses.Queries.GetStreetListQuery;
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
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.Address, EndpointType = EndpointType.Reading, Definition = "Get Address Index Page")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.Address, EndpointType = EndpointType.Reading, Definition = "Get By Id Address for Create Address")]
    public async Task<IActionResult> CreateAddress(int userId)
    {
        var dresult = await _mediator.Send(new GetCreateAddressQueryRequest()
        {
            UserId = userId
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return View(dresult.Result.Data);
        }
        return RedirectToAction("Index", "Error" ,new { area = "", statusCode = 400});
    }

    [HttpPost]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.Address, EndpointType = EndpointType.Writing, Definition = "Create Address")]
    public async Task<IActionResult> CreateAddress(AddressDto addressDto)
    {
        if (ModelState.IsValid)
        {
            var dresult = await _mediator.Send(new CreateAddressCommandRequest
            {
                AddressDto = addressDto
            });
            if (dresult.Result.Message == Messages.AddressCountMoreThan4)
            {
                var getSelectedAddressResult = await _mediator.Send(new GetSelectedAddressQueryRequest()
                {
                    AddressDto = addressDto
                });
                ModelState.AddModelError("AddressCountMoreThan4", Messages.AddressCountMoreThan4);
                return PartialView("PartialViews/_CreateAddressPartial", getSelectedAddressResult.Result.Data);
            }
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true});
            }
        }
        var selectedAddressResult = await _mediator.Send(new GetSelectedAddressQueryRequest()
        {
            AddressDto = addressDto
        });
        return PartialView("PartialViews/_CreateAddressPartial", selectedAddressResult.Result.Data);
    }

    [HttpGet]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.Address, EndpointType = EndpointType.Reading, Definition = "Get By Id Address for Update Address")]
    public async Task<IActionResult> UpdateAddress(int addressId)
    {
        if (addressId > 0)
        {
            var dresult = await _mediator.Send(new GetByIdUpdateAddressQueryRequest()
            {
                Id = addressId
            });
            if (dresult.Result.ResultStatus==ResultStatus.Success)
            {
                return View(dresult.Result.Data);
            }
            
        }
        return RedirectToAction("Index", "Error" ,new { area = "", statusCode = 400});
    }

    [HttpPost]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.Address, EndpointType = EndpointType.Updating, Definition = "Update Address")]
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
                TempData["UpdateAddressMessage"] = true;
                return Json(new { success = true});
            }
        }
        var selectedAddressResult = await _mediator.Send(new GetSelectedAddressQueryRequest()
        {
            AddressDto = addressDto
        });
        return PartialView("PartialViews/_UpdateAddressPartial", selectedAddressResult.Result.Data);
    }


    [HttpGet]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.Address, EndpointType = EndpointType.Reading, Definition = "Get By Id Address for Address Details")]
    public async Task<IActionResult>  DetailAddress(int addressId)
    {
        if (addressId > 0)
        {
            var dresult = await _mediator.Send(new GetByIdDetailAddressQueryRequest()
            {
                Id = addressId
            });
            if (dresult.Result.ResultStatus==ResultStatus.Success)
            {
                return View(dresult.Result.Data);
            }
        }
        return RedirectToAction("Index", "Error" ,new { area = "", statusCode = 400});
    }
    
    
    [HttpPost]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.Address, EndpointType = EndpointType.Deleting, Definition = "Delete Address")]
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
        return RedirectToAction("Index", "Error" ,new { area = "", statusCode = 400});
    }    
    
    
    [HttpPost]
    public async Task<IActionResult> GetAllDistrictsByCityId(int cityId)
    {
        var dresult = await _mediator.Send(new GetDistrictListQueryRequest()
        {
            CityId = cityId
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true, districts = dresult.Result.Data});
        }
        return Json(new { success = false });
    }
    
    [HttpPost]
    public async Task<IActionResult> GetAllNeighborhoodsOrVillagesByDistrictId(int districtId)
    {
        var dresult = await _mediator.Send(new GetNeighborhoodOrVillageListQueryRequest()
        {
            DistrictId = districtId
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true, neighborhoodsOrVillages = dresult.Result.Data});
        }
        return Json(new { success = false });
    }
    
    [HttpPost]
    public async Task<IActionResult> GetAllStreetsByNeighborhoodOrVillageId(int neighborhoodOrVillageId)
    {
        var dresult = await _mediator.Send(new GetStreetListQueryRequest()
        {
            NeighborhoodOrVillageId = neighborhoodOrVillageId
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true, streets = dresult.Result.Data});
        }
        return Json(new { success = false });
    }
}