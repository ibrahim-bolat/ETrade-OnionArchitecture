using System.Web;
using AutoMapper;
using ETrade.Application.Features.Accounts.Constants;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.Accounts.Commands.ConfirmEmailUserCommand;

public class ConfirmEmailUserCommandHandler:IRequestHandler<ConfirmEmailUserCommandRequest,ConfirmEmailUserCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;

    public ConfirmEmailUserCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<ConfirmEmailUserCommandResponse> Handle(ConfirmEmailUserCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return new ConfirmEmailUserCommandResponse{
                Result = new Result(ResultStatus.Error, Messages.UserNotFound)
            };
        }
        var result = await _userManager.ConfirmEmailAsync(user, HttpUtility.UrlDecode(request.Token));
        if (result.Succeeded)
        {
            return new ConfirmEmailUserCommandResponse{
                Result = new Result(ResultStatus.Success, Messages.UserConfirmEmail)
            };
        }
        return new ConfirmEmailUserCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.UserNotConfirmEmail,result.Errors.ToList())
        };
    }
}