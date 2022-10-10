using AutoMapper;
using ETrade.Application.DTOs.UserDtos;
using ETrade.Application.Services.Abstract;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace ETrade.MVC.Areas.Admin.ViewComponents;

[ViewComponent]
public class UserSummaryCardViewComponent : ViewComponent
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserImageService _userImageService;
    private readonly IMapper _mapper;

    public UserSummaryCardViewComponent(UserManager<AppUser> userManager, IUserImageService userImageService,
        IMapper mapper)
    {
        _userManager = userManager;
        _userImageService = userImageService;
        _mapper = mapper;
    }

    public async Task<IViewComponentResult> InvokeAsync(int userId)
    {
        AppUser user = await _userManager.FindByIdAsync(userId.ToString());
        var countResult = await _userImageService.GetUserImageCountByUserIdAsync(userId);
        if (countResult.ResultStatus == ResultStatus.Success)
            ViewBag.UserImageCount = countResult.Data;

        var profilResult = await _userImageService.GetProfilImageByUserIdAsync(userId);
        if (profilResult.ResultStatus == ResultStatus.Success)
        {
            if (profilResult.Data != null)
                ViewBag.UserImageProfilImagePath = profilResult.Data.ImagePath;

            if (countResult.Data > 0)
            {
                var dresult = await _userImageService.GetAllByUserIdAsync(userId);
                if (dresult.ResultStatus == ResultStatus.Success)
                {
                    ViewBag.UserImageList = dresult.Data;
                }
            }
        }
        else
        {
            if (countResult.Data > 0)
            {
                var dresult = await _userImageService.GetAllByUserIdAsync(userId);
                if (dresult.ResultStatus == ResultStatus.Success)
                {
                    ViewBag.UserImageList = dresult.Data;
                }
            }
        }
        UserCardSummaryDto userCardSummaryDto = _mapper.Map<UserCardSummaryDto>(user);
        return View(userCardSummaryDto);
    }
}