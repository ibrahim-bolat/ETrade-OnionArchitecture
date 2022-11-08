using ETrade.Application.Features.Accounts.Queries.GetByUserNameUserImageQuery;
using ETrade.Application.Features.UserOperations.Queries.GetByIdForUserSummaryQuery;
using ETrade.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETrade.MVC.Areas.Admin.ViewComponents;

[ViewComponent]
public class AdminHeaderAvatarViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public AdminHeaderAvatarViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var imageResult = await _mediator.Send(new GetByUserNameUserImageQueryRequest()
            {
                UserName = User.Identity?.Name,
            });
            ViewBag.UserId = imageResult.Result.Data.Select(i => i.UserId).FirstOrDefault();
            ViewBag.ProfilPhoto = "/admin/images/avatar/unspecifieduseravatar.png";
            var userResult = await _mediator.Send(new GetByIdForUserSummaryQueryRequest()
            {
                Id = ViewBag.UserId.ToString()
            });
            ViewBag.FullName = userResult.Result.Data.FirstName + " " + userResult.Result.Data.LastName;
            if (imageResult.Result.Data.Count > 0)
            {
                foreach (var userImage in imageResult.Result.Data)
                {
                    if (userImage.Profil)
                        ViewBag.ProfilPhoto = userImage.ImagePath;
                }
            }
            return View();
        }
    }
