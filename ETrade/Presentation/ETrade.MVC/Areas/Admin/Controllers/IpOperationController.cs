using ETrade.Application.Constants;
using ETrade.Application.CustomAttributes;
using ETrade.Application.DTOs.Common;
using ETrade.Application.Features.IpOperations.Commands.CreateIpAddressCommand;
using ETrade.Application.Features.IpOperations.Commands.SetIpAddressActiveCommand;
using ETrade.Application.Features.IpOperations.Commands.SetIpAddressPassiveCommand;
using ETrade.Application.Features.IpOperations.Commands.UpdateIpAddressCommand;
using ETrade.Application.Features.IpOperations.DTOs;
using ETrade.Application.Features.IpOperations.Queries.GetByIdIpAddressQuery;
using ETrade.Application.Features.IpOperations.Queries.GetIpAddressListQuery;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Messages = ETrade.Application.Features.IpOperations.Constants.Messages;


namespace ETrade.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class IpOperationController : Controller
{
    private readonly IMediator _mediator;

    public IpOperationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> GetAllIpAddresses(DatatableRequestDto datatableRequestDto)
    {
        var dresult = await _mediator.Send(new GetIpAddressListQueryRequest()
        {
            DatatableRequestDto = datatableRequestDto
        });

        var jsonData = new
        {
            draw = dresult.Result.Data.Draw, recordsFiltered = dresult.Result.Data.RecordsFiltered,
            recordsTotal = dresult.Result.Data.RecordsTotal, data = dresult.Result.Data.Data, isSusccess = true
        };
        return Ok(jsonData);
    }


    [HttpPost]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.IpOperation, EndpointType = EndpointType.Writing,
        Definition = "Create IpAddress")]
    public async Task<IActionResult> CreateIpAddress(IpDto ipDto)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("PartialViews/_IpCreateModalPartial", ipDto);
        }

        var dresult = await _mediator.Send(new CreateIpAddressCommandRequest()
        {
            IpDto = ipDto
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true });
        }

        return Json(new { success = false });
    }


    [HttpPost]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.IpOperation, EndpointType = EndpointType.Updating,
        Definition = "Update IpAddress")]
    public async Task<IActionResult> UpdateIpAddress(IpDto ipDto)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("PartialViews/_IpUpdateModalPartial", ipDto);
        }

        var dresult = await _mediator.Send(new UpdateIpAddressCommandRequest()
        {
            IpDto = ipDto
        });
        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.IpNotFound))
        {
            ModelState.AddModelError("IpNotFound", Messages.IpNotFound);
            return PartialView("PartialViews/_IpUpdateModalPartial", ipDto);
        }

        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true });
        }

        return Json(new { success = false });
    }

    [HttpPost]
    public async Task<IActionResult> SetIpAddressActive(int id)
    {
        var dresult = await _mediator.Send(new SetIpAddressActiveCommandRequest()
        {
            Id = id
        });

        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true });
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.IpNotFound))
        {
            ModelState.AddModelError("IpNotFound", Messages.IpNotFound);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.IpActive))
        {
            ModelState.AddModelError("IpActive", Messages.IpActive);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }

        return Json(new { success = false });
    }

    [HttpPost]
    public async Task<IActionResult> SetIpAddressPassive(int id)
    {
        var dresult = await _mediator.Send(new SetIpAddressPassiveCommandRequest()
        {
            Id = id
        });

        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true });
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.IpNotFound))
        {
            ModelState.AddModelError("IpNotFound", Messages.IpNotFound);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error &&
            dresult.Result.Message.Equals(Messages.IpNotActive))
        {
            ModelState.AddModelError("IpNotActive", Messages.IpNotActive);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }

        return Json(new { success = false });
    }

    [HttpGet]
    [AuthorizeEndpoint(Menu = AuthorizeEndpointConstants.IpOperation, EndpointType = EndpointType.Reading,
        Definition = "Get By Id IpAddress Details")]
    public async Task<IActionResult> GetIpAddressById(int id)
    {
        var dresult = await _mediator.Send(new GetByIdIpAddressQueryRequest()
        {
            Id = id
        });
        if (dresult.Result.ResultStatus == ResultStatus.Success)
        {
            return Json(new { success = true, ip = dresult.Result.Data });
        }

        if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.IpNotFound))
        {
            ModelState.AddModelError("IpNotFound", Messages.IpNotFound);
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }

        return Json(new { success = false });
    }
}