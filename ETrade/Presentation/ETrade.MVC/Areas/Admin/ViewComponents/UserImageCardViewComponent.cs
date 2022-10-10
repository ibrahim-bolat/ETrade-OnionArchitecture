using ETrade.Application.Services.Abstract;
using ETrade.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.MVC.Areas.Admin.ViewComponents;

[ViewComponent]
public class UserImageCardViewComponent : ViewComponent
    {
        private readonly IUserImageService _userImageService;

        public UserImageCardViewComponent(IUserImageService userImageService)
        {
            _userImageService = userImageService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int userId)
        {
            if (userId > 0)
            {
                var dresult = await _userImageService.GetAllByUserIdAsync(userId);
                if (dresult.ResultStatus==ResultStatus.Success)
                {
                    ViewBag.UserId = userId;
                    return View(dresult.Data);
                }
            }
            return View();
        }
    }
