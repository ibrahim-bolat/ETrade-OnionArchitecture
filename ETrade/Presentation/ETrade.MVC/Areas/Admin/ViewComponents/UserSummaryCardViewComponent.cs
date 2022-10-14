using ETrade.Application.Features.Accounts.Queries.GetByIdForUserSummaryCardQuery;
using ETrade.Application.Features.UserImages.Queries.GetByUserIdAllUserImageQuery;
using ETrade.Application.Features.UserImages.Queries.GetByUserIdProfilImageQuery;
using ETrade.Application.Features.UserImages.Queries.GetByUserIdUserImageCountQuery;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace ETrade.MVC.Areas.Admin.ViewComponents;

[ViewComponent]
public class UserSummaryCardViewComponent : ViewComponent
{

    private readonly IMediator _mediator;

    public UserSummaryCardViewComponent(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IViewComponentResult> InvokeAsync(int userId)
    {
        var countResult = await _mediator.Send(new GetByUserIdUserImageCountQueryRequest()
        {
            UserId = userId
        });
        if (countResult.Result.ResultStatus == ResultStatus.Success)
            ViewBag.UserImageCount = countResult.Result.Data;
        
        var profilResult = await _mediator.Send(new GetByUserIdProfilImageQueryRequest()
        {
            UserId = userId
        });
        if (profilResult.Result.ResultStatus == ResultStatus.Success)
        {
            if (profilResult.Result.Data != null)
                ViewBag.UserImageProfilImagePath = profilResult.Result.Data.ImagePath;

            if (countResult.Result.Data > 0)
            {
                var allImageResult = await _mediator.Send(new GetByUserIdAllUserImageQueryRequest()
                {
                    UserId = userId
                });
                if (allImageResult.Result.ResultStatus == ResultStatus.Success)
                {
                    ViewBag.UserImageList = allImageResult.Result.Data;
                }
            }
        }
        else
        {
            if (countResult.Result.Data > 0)
            {
                var allImageResult = await _mediator.Send(new GetByUserIdAllUserImageQueryRequest()
                {
                    UserId = userId
                });
                if (allImageResult.Result.ResultStatus == ResultStatus.Success)
                {
                    ViewBag.UserImageList = allImageResult.Result.Data;
                }
            }
        }
        var userCardSummaryResult = await _mediator.Send(new GetByIdForUserSummaryCardQueryRequest()
        {
            Id = userId
        });
        return View(userCardSummaryResult.Result.Data);
    }
}