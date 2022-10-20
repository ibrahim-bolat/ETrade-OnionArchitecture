using AutoMapper;
using ETrade.Application.Features.Accounts.Constants;
using ETrade.Application.Features.Accounts.DTOs;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.Accounts.Commands.RegisterUserCommand;

public class RegisterUserCommandHandler:IRequestHandler<RegisterUserCommandRequest,RegisterUserCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;

    public RegisterUserCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
    {
        AppUser applicationUser = _mapper.Map<AppUser>(request.RegisterDto);
        AppRole role = await _roleManager.FindByNameAsync(RoleType.User.ToString());
        if (role == null)
            await _roleManager.CreateAsync(new AppRole { Name = RoleType.User.ToString() });
        IdentityResult userResult = await _userManager.CreateAsync(applicationUser, request.RegisterDto.Password);
        IdentityResult roleResult;
        if (userResult.Succeeded)
        {
            roleResult = await _userManager.AddToRoleAsync(applicationUser, RoleType.User.ToString());
            if (roleResult.Succeeded)
            {
                return new RegisterUserCommandResponse
                {
                    Result = new DataResult<RegisterDto>(ResultStatus.Success, Messages.UserAdded, request.RegisterDto)
                };
            }
            return new RegisterUserCommandResponse{
                Result = new Result(ResultStatus.Error, Messages.RoleNotAdded,roleResult.Errors.ToList())
            };
        }
        return new RegisterUserCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.UserNotAdded,userResult.Errors.ToList())
        };
    }
}