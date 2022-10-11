using AutoMapper;
using ETrade.Application.Features.Accounts.DTOs.RoleDtos;
using ETrade.Application.Features.Accounts.DTOs.UserDtos;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace ETrade.MVC.Areas.Admin.Controllers;


[Area("Admin")]
    public class UserOperationController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public UserOperationController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Users()
        {
            try
            {
                var userData = _userManager.Users.Where(u=>u.IsActive==true).AsQueryable();
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request
                    .Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length == "-1" ? userData.Count() : length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal;
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    userData = userData.OrderBy(s => sortColumn + " " + sortColumnDirection);
                    Func<AppUser, string> orderingFunction = (c => sortColumn == "FirstName" ? c.FirstName :
                        sortColumn == "LastName" ? c.LastName :
                        sortColumn == "UserName" ? c.UserName :
                        sortColumn == "Email" ? c.Email : c.FirstName);

                    if (sortColumnDirection == "desc")
                    {
                        userData = userData.OrderByDescending(orderingFunction).AsQueryable();
                    }
                    else
                    {
                        userData = userData.OrderBy(orderingFunction).AsQueryable();
                    }
                }

                if (!string.IsNullOrEmpty(searchValue))
                {
                    userData = userData.Where(m => m.FirstName.ToLower().Contains(searchValue.ToLower())
                                                   || m.LastName.ToLower().Contains(searchValue.ToLower())
                                                   || m.UserName.ToLower().Contains(searchValue.ToLower())
                                                   || m.Email.ToLower().Contains(searchValue.ToLower()));
                }

                recordsTotal = userData.Count();
                var data = userData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new
                {
                    draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data, 
                    isSusccess = true
                };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("PartialViews/_UserCreateModalPartial", registerDto);
            }
            IdentityResult createResult, confirmResult, roleResult;
            AppUser newUsers = _mapper.Map<AppUser>(registerDto);
            AppRole role = await _roleManager.FindByNameAsync(RoleType.User.ToString());
            if (role == null)
                await _roleManager.CreateAsync(new AppRole { Name = RoleType.User.ToString() });
            createResult = await _userManager.CreateAsync(newUsers, registerDto.Password);
            if (createResult.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUsers);
                confirmResult = await _userManager.ConfirmEmailAsync(newUsers, token);
                if (confirmResult.Succeeded)
                {
                    roleResult = await _userManager.AddToRoleAsync(newUsers, RoleType.User.ToString());
                    if (roleResult.Succeeded)
                    {
                        return Json(new { success = true });
                    }
                    roleResult.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));   
                    return PartialView("PartialViews/_UserCreateModalPartial", registerDto);   
                }
                confirmResult.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                return PartialView("PartialViews/_UserCreateModalPartial", registerDto);
            }
            createResult.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
            return PartialView("PartialViews/_UserCreateModalPartial", registerDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetbyId(string id)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (appUser != null)
            {
                UserSummaryDto model = _mapper.Map<UserSummaryDto>(appUser);
                return Json(new { success = true, user = model });
            }
            ModelState.AddModelError("NoUser", "Bu bilgilere sahip bir kullanıcı bulunamadı.");
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityResult result;
            AppUser deletedUser = await _userManager.FindByIdAsync(id);
            if (deletedUser != null)
            {
                deletedUser.IsActive = false;
                deletedUser.IsDeleted = true;
                deletedUser.ModifiedTime = DateTime.Now;
                deletedUser.ModifiedByName = User.Identity?.Name;
                result = await _userManager.UpdateAsync(deletedUser);
                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                    return Json(new { success = false});
                }
                result = await _userManager.UpdateSecurityStampAsync(deletedUser);
                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                    return Json(new { success = false});
                }
                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }
            }
            //result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }

        [HttpGet]
        public async Task<IActionResult> GetRole(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                List<AppRole> allRoles = _roleManager.Roles.ToList();
                List<string> userRoles = await _userManager.GetRolesAsync(user) as List<string>;
                List<RoleOperationDto> assignRoles = new List<RoleOperationDto>();
                allRoles.ForEach(role => assignRoles.Add(new RoleOperationDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    HasAssign = userRoles != null && userRoles.Contains(role.Name)
                }));
                return Json(new { success = true, roles = assignRoles });
            }
            return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
        }

        [HttpPost]
        public async Task<IActionResult> SaveRole(string id, List<String> roles)
        {
            if (!string.IsNullOrEmpty(id) && roles != null)
            {
                AppUser user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    List<AppRole> allRoles = _roleManager.Roles.ToList();
                    List<RoleOperationDto> newAssignRoles = new List<RoleOperationDto>();
                    List<string> userNewRoles = new List<string>();
                    AppRole appR;
                    foreach (var role in roles)
                    {
                        appR = await _roleManager.FindByIdAsync(role);
                        userNewRoles.Add(appR.Name);
                    }
                    allRoles.ForEach(role => newAssignRoles.Add(new RoleOperationDto
                    {
                        HasAssign = userNewRoles.Contains(role.Name),
                        Id = role.Id,
                        Name = role.Name
                    }));
                    foreach (RoleOperationDto role in newAssignRoles)
                    {
                        if (role.HasAssign)
                        {
                            if (!await _userManager.IsInRoleAsync(user, role.Name))
                                await _userManager.AddToRoleAsync(user, role.Name);
                        }
                        else
                        {
                            if (await _userManager.IsInRoleAsync(user, role.Name))
                                await _userManager.RemoveFromRoleAsync(user, role.Name);
                        }
                    }
                    await _userManager.UpdateSecurityStampAsync(user);
                    var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                    if (currentUser.Id == user.Id)
                    {
                        await _signInManager.SignOutAsync();
                        await _signInManager.SignInAsync(user, true);
                    }
                    return Json(new { success = true });
                }
                return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
            }
            return Json(new { success = false });
        }
    }