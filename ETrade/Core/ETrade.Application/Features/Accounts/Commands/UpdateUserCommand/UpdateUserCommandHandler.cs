using AutoMapper;
using ETrade.Application.Features.Accounts.Constants;
using ETrade.Application.Features.Accounts.DTOs.UserDtos;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.Accounts.Commands.UpdateUserCommand;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, UpdateUserCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        IdentityResult result;
        AppUser user;
        string tempOldUserName = String.Empty;
        if (request.UserDto.Email.Equals(request.OldEmail))
        {
            user = await _userManager.FindByEmailAsync(request.UserDto.Email);
            if (user != null)
            {
                if (user.IsActive)
                {
                    user = _mapper.Map(request.UserDto, user);
                }
                else
                {
                    return new UpdateUserCommandResponse
                    {
                        Result = new Result(ResultStatus.Error, Messages.UserNotActive)
                    };
                }
            }
            else
            {
                return new UpdateUserCommandResponse
                {
                    Result = new Result(ResultStatus.Error, Messages.UserNotFound)
                };
            }
        }
        else
        {
            user = await _userManager.FindByEmailAsync(request.OldEmail);
            if (user != null)
            {
                if (user.IsActive)
                {
                    tempOldUserName = user.UserName;
                    user = _mapper.Map(request.UserDto, user);
                    result = await _userManager.SetEmailAsync(user, request.UserDto.Email);
                    if (!result.Succeeded)
                    {
                        return new UpdateUserCommandResponse
                        {
                            Result = new Result(ResultStatus.Error, Messages.UserNotUpdateEmail, result.Errors.ToList())
                        };
                    }
                }
                else
                {
                    return new UpdateUserCommandResponse
                    {
                        Result = new Result(ResultStatus.Error, Messages.UserNotActive)
                    };
                }
            }
            else
            {
                return new UpdateUserCommandResponse
                {
                    Result = new Result(ResultStatus.Error, Messages.UserNotFound)
                };
            }
        }

        result = await _userManager.SetUserNameAsync(user, request.UserDto.UserName);
        if (!result.Succeeded)
        {
            return new UpdateUserCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.UserNotUpdateUserName, result.Errors.ToList())
            };
        }

        result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return new UpdateUserCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.UserNotUpdated, result.Errors.ToList())
            };
        }

        result = await _userManager.UpdateSecurityStampAsync(user);
        if (!result.Succeeded)
        {
            return new UpdateUserCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.UserNotUpdateSecurityStamp, result.Errors.ToList())
            };
        }
        string userIdentityName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
        if (!String.IsNullOrEmpty(userIdentityName) && !String.IsNullOrEmpty(tempOldUserName) && userIdentityName.Equals(tempOldUserName) && request.UserDto.Email != request.OldEmail)
        {
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(user, true);
        }
        return new UpdateUserCommandResponse
        {
            Result = new DataResult<UserDto>(ResultStatus.Success, Messages.UserUpdated, request.UserDto)
        };
    }
}