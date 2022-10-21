using ETrade.Application.Features.UserImages.Queries.GetByUserIdAllUserImageQuery;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.MVC.Areas.Admin.ViewComponents;

[ViewComponent]
public class UserImageCardViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public UserImageCardViewComponent(IMediator mediator)
        {
             _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int userId)
        {
            if (userId > 0)
            {
                var dresult = await _mediator.Send(new GetByUserIdAllUserImageQueryRequest()
                {
                    UserId = userId,
                });
                if (dresult.Result.ResultStatus == ResultStatus.Success)
                {
                    ViewBag.UserId = userId;
                    return View(dresult.Result.Data.OrderBy(i=>i.Id).ToList());
                }
            }
            return View();
        }
    }
