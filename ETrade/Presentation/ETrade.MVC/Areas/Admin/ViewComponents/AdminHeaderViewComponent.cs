using ETrade.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETrade.MVC.Areas.Admin.ViewComponents;

[ViewComponent]
public class AdminHeaderAvatarViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminHeaderAvatarViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            AppUser appUser = await _userManager.Users.Include(x => x.UserImages)
                .FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
            ViewBag.UserId = appUser?.Id;
            ViewBag.ProfilPhoto = "/admin/images/avatar/unspecifieduseravatar.png";
            if (appUser?.UserImages?.Count > 0)
            {
                foreach (var userImage in appUser.UserImages)
                {
                    if (userImage.Profil && userImage.IsActive)
                        ViewBag.ProfilPhoto = userImage.ImagePath;
                }
            }
            return View();
        }
    }
