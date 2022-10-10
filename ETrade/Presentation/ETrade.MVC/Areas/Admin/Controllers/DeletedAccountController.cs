using ETrade.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class DeletedAccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;

    public DeletedAccountController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult DeletedUsers()
    {
        try
        {
            var userData = _userManager.Users.Where(u => u.IsDeleted == true).AsQueryable();
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
                draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data, isSusccess = true
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
    public async Task<IActionResult> SetActiveUser(int userId)
    {
        AppUser user = await _userManager.FindByIdAsync(userId.ToString());
        if (user != null)
        {
            if (user.IsDeleted)
            {
                user.IsActive = true;
                user.IsDeleted = false;
                user.ModifiedTime = DateTime.Now;
                user.ModifiedByName = User.Identity?.Name;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                    return Json(new { success = false});
                }
                result = await _userManager.UpdateSecurityStampAsync(user);
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
            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value?.Errors);
            return Json(new { success = false, errors = errors });
        }
        return RedirectToAction("AllErrorPages", "ErrorPages" ,new { area = "", statusCode = 404});
    }
}