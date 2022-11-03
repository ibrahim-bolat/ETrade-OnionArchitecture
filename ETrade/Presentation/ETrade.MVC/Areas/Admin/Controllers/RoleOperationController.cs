using ETrade.Application.Constants;
using ETrade.Application.CustomAttributes;
using ETrade.Application.DTOs.Common;
using ETrade.Application.Features.RoleOperations.Commands.CreateRoleCommand;
using ETrade.Application.Features.RoleOperations.Commands.RemoveUserFromRoleCommand;
using ETrade.Application.Features.RoleOperations.Commands.SetRoleActiveCommand;
using ETrade.Application.Features.RoleOperations.Commands.SetRolePassiveCommand;
using ETrade.Application.Features.RoleOperations.Commands.UpdateRoleCommand;
using ETrade.Application.Features.RoleOperations.DTOs;
using ETrade.Application.Features.RoleOperations.Queries.GetAuthorizeDefinitionEndpointsQuery;
using ETrade.Application.Features.RoleOperations.Queries.GetByIdRoleQuery;
using ETrade.Application.Features.RoleOperations.Queries.GetRoleListQuery;
using ETrade.Application.Features.RoleOperations.Queries.GetUsersOfTheRoleQuery;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace ETrade.MVC.Areas.Admin.Controllers;


[Area("Admin")]
    public class RoleOperationController : Controller
    {
        private readonly IMediator _mediator;

        public RoleOperationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.RoleOperation, ActionType = ActionType.Reading, Definition = "Get RoleOperation Index Page")]
        public IActionResult  Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Roles(DatatableRequestDto datatableRequestDto)
        {
            
            var dresult = await _mediator.Send(new GetRoleListQueryRequest()
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
        
        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.RoleOperation, ActionType = ActionType.Reading, Definition = "Get Authorization Index Page")]
        public  IActionResult Authorization()
        {
            return View();
        }
        
        [HttpGet]
        public  async Task<IActionResult>  GetPermission(string query)
        {
            var dresult = await _mediator.Send(new GetAuthorizeDefinitionEndpointsQueryRequest()
            {
                Type = typeof(Program)
            });
            
            var jsonData = dresult.Result.Data;

                if (!string.IsNullOrWhiteSpace(query))
                {
                    jsonData = jsonData.Where(q => q.Name.Contains(query)).ToList();
                }
                
                return  Json(jsonData);
        }
        /*
    private List<Menu> GetActions(List<Menu> menus, int parentId)
    {
        return menus.Where(l => l.ParentID == parentId).OrderBy(l => l.OrderNumber)
            .Select(l => new Models.DTO.Location
            {
                id = l.ID,
                text = l.Name,
                population = l.Population,
                flagUrl = l.FlagUrl,
                @checked = l.Checked,
                children = GetChildren(locations, l.ID)
            }).ToList();
    }

    public JsonResult LazyGet(int? parentId)
    {
        List<Location> locations;
        List<Models.DTO.Location> records;
        using (ApplicationDbContext context = new ApplicationDbContext())
        {
            locations = context.Locations.ToList();

            records = locations.Where(l => l.ParentID == parentId).OrderBy(l => l.OrderNumber)
                .Select(l => new Models.DTO.Location
                {
                    id = l.ID,
                    text = l.Name,
                    @checked = l.Checked,
                    population = l.Population,
                    flagUrl = l.FlagUrl,
                    hasChildren = locations.Any(l2 => l2.ParentID == l.ID)
                }).ToList();
        }

        return this.Json(records, JsonRequestBehavior.AllowGet);
    }
    

    
    [HttpPost]
    public  IActionResult SavePermission(List<int> checkedIds)
    {
        if (checkedIds == null)
        {
            checkedIds = new List<int>();
        }
        using (ApplicationDbContext context = new ApplicationDbContext())
        {
            var locations = context.Locations.ToList();
            foreach (var location in locations)
            {
                location.Checked = checkedIds.Contains(location.ID);
            }
            context.SaveChanges();
        }

        return Json(true);
    }
    
    [HttpPost]
    public JsonResult ChangeNodePosition(int id, int parentId, int orderNumber)
    {
        using (ApplicationDbContext context = new ApplicationDbContext())
        {
            var location = context.Locations.First(l => l.ID == id);

            var newSiblingsBelow = context.Locations.Where(l => l.ParentID == parentId && l.OrderNumber >= orderNumber);
            foreach (var sibling in newSiblingsBelow)
            {
                sibling.OrderNumber = sibling.OrderNumber + 1;
            }

            var oldSiblingsBelow = context.Locations.Where(l => l.ParentID == location.ParentID && l.OrderNumber > location.OrderNumber);
            foreach (var sibling in oldSiblingsBelow)
            {
                sibling.OrderNumber = sibling.OrderNumber - 1;
            }


            location.ParentID = parentId;
            location.OrderNumber = orderNumber;

            context.SaveChanges();
        }

        return this.Json(true);
    }
    */
        
        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.RoleOperation, ActionType = ActionType.Reading, Definition = "Get Users Of TheRole Index Page")]
        public  async Task<IActionResult> UsersOfTheRole(int id)
        {
            var dresult = await _mediator.Send(new GetByIdRoleQueryRequest()
            {
                Id=id.ToString()
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return View(dresult.Result.Data);
            }
            return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
        }
        
        [HttpPost]
        public  async Task<IActionResult> UsersOfTheRole(DatatableRequestDto datatableRequestDto,[FromQuery]string id)
        {
            var dresult = await _mediator.Send(new GetUsersOfTheRoleQueryRequest()
            {
                Id=id,
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
        public  async Task<IActionResult> RemoveUserFromRole(int userId,int roleId)
        {
            var dresult = await _mediator.Send(new RemoveUserFromRoleCommandRequest()
            {
                UserId = userId.ToString(),
                RoleId = roleId.ToString()
            });

            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.UserNotFound))
            {
                ModelState.AddModelError("UserNotFound", Messages.UserNotFound);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.UserNotActive))
            {
                ModelState.AddModelError("UserNotActive", Messages.UserNotActive);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList!=null)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.RoleOperation, ActionType = ActionType.Writing, Definition = "Create Role")]
        public async Task<IActionResult> CreateRole(RoleDto roleDto)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("PartialViews/_RoleCreateModalPartial", roleDto);
            }
            var dresult = await _mediator.Send(new CreateRoleCommandRequest()
            {
                RoleDto = roleDto
            });
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList!=null)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                return PartialView("PartialViews/_RoleCreateModalPartial", roleDto);   
            }
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.RoleOperation, ActionType = ActionType.Updating, Definition = "Update Role")]
        public async Task<IActionResult> UpdateRole(RoleDto roleDto)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("PartialViews/_RoleUpdateModalPartial", roleDto);
            }
            var dresult = await _mediator.Send(new UpdateRoleCommandRequest()
            {
                RoleDto = roleDto
            });
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList!=null)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                return PartialView("PartialViews/_RoleUpdateModalPartial", roleDto);   
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.RoleNotFound))
            {
                ModelState.AddModelError("RoleActive", Messages.RoleNotFound);
                return PartialView("PartialViews/_RoleUpdateModalPartial", roleDto);   
            }
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        
        [HttpPost]
        public async Task<IActionResult> SetRoleActive(string id)
        {
            var dresult = await _mediator.Send(new SetRoleActiveCommandRequest()
            {
                Id = id
            });

            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.RoleNotFound))
            {
                ModelState.AddModelError("RoleNotFound", Messages.RoleNotFound);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.RoleActive))
            {
                ModelState.AddModelError("RoleActive", Messages.RoleActive);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList!=null)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            return Json(new { success = false });
        }
        
        [HttpPost]
        public async Task<IActionResult> SetRolePassive(string id)
        {
            var dresult = await _mediator.Send(new SetRolePassiveCommandRequest()
            {
                Id = id
            });

            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.RoleNotFound))
            {
                ModelState.AddModelError("RoleNotFound", Messages.RoleNotFound);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error &&
                dresult.Result.Message.Equals(Messages.RoleNotActive))
            {
                ModelState.AddModelError("RoleNotActive", Messages.RoleNotActive);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.IdentityErrorList!=null)
            {
                dresult.Result.IdentityErrorList.ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            return Json(new { success = false });
        }
        
        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.RoleOperation, ActionType = ActionType.Reading, Definition = "Get By Id Role Details")]
        public async Task<IActionResult> GetRole(string id)
        {
            var dresult = await _mediator.Send(new GetByIdRoleQueryRequest()
            {
                Id = id
            });
            if (dresult.Result.ResultStatus == ResultStatus.Success)
            {
                return Json(new { success = true, role = dresult.Result.Data });
            }
            if (dresult.Result.ResultStatus == ResultStatus.Error && dresult.Result.Message.Equals(Messages.RoleNotFound))
            {
                ModelState.AddModelError("RoleNotFound", Messages.RoleNotFound);
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
                return Json(new { success = false, errors = errors });
            }
            return Json(new { success = false });
        }
    }
