using ETrade.Application.Features.Accounts.DTOs.RoleDtos;
using ETrade.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.MVC.Areas.Admin.Controllers;

[Area("Admin")]
    public class RoleController : Controller
    {
        readonly RoleManager<AppRole> _roleManager;
        readonly UserManager<AppUser> _userManager;
        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        
        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }
        
        public async Task<IActionResult> CreateRole(string id)
        {
            if (id != null)
            {
                AppRole role = await _roleManager.FindByIdAsync(id);

                return View(new RoleDto()
                {
                    Name = role.Name
                });
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleDto model, string id)
        {
            IdentityResult result;
            if (id != null)
            {
                AppRole role = await _roleManager.FindByIdAsync(id);
                role.Name = model.Name;
                result = await _roleManager.UpdateAsync(role);
            }
            else
                result = await _roleManager.CreateAsync(new AppRole { Name = model.Name});

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Role" ,new {area="Admin"});
            }
            return View();
        }
        
        public async Task<IActionResult> RoleAssign(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            List<AppRole> allRoles = _roleManager.Roles.ToList();
            List<string> userRoles = await _userManager.GetRolesAsync(user) as List<string>;
            List<RoleAssignDto> assignRoles = new List<RoleAssignDto>();
            allRoles.ForEach(role => assignRoles.Add(new RoleAssignDto
            {
                HasAssign = userRoles != null && userRoles.Contains(role.Name),
                RoleId = role.Id,
                RoleName = role.Name
            }));

            return View(assignRoles);
        }
        [HttpPost]
        public async Task<ActionResult> RoleAssign(List<RoleAssignDto> modelList, string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            foreach (RoleAssignDto role in modelList)
            {
                if (role.HasAssign)
                    await _userManager.AddToRoleAsync(user, role.RoleName);
                else
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);
            }
            return RedirectToAction("Index", "Home" ,new {area="Admin"});
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            IdentityResult result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                //Başarılı...
            }
            return RedirectToAction("Index");
        }

    }