using AutoMapper;
using ETrade.Application.Features.UserOperations.Constants;
using ETrade.Application.Features.UserOperations.DTOs;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.UserOperations.Commands.CreateUserCommand;

public class CreateUserCommandHandler:IRequestHandler<CreateUserCommandRequest,CreateUserCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        IdentityResult createResult, confirmResult, roleResult;
        AppUser newUser = _mapper.Map<AppUser>(request.CreateUserDto);
        AppRole role = await _roleManager.FindByNameAsync(RoleType.User.ToString());
        if (role == null)
            await _roleManager.CreateAsync(new AppRole { Name = RoleType.User.ToString() });
        createResult = await _userManager.CreateAsync(newUser, request.CreateUserDto.Password);
        if (createResult.Succeeded)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            confirmResult = await _userManager.ConfirmEmailAsync(newUser, token);
            if (confirmResult.Succeeded)
            {
                roleResult = await _userManager.AddToRoleAsync(newUser, RoleType.User.ToString());
                if (roleResult.Succeeded)
                {
                    return new CreateUserCommandResponse
                    {
                        Result = new DataResult<CreateUserDto>(ResultStatus.Success, Messages.UserAdded, request.CreateUserDto)
                    };
                }
                return new CreateUserCommandResponse{
                    Result = new Result(ResultStatus.Error, Messages.UserErrorConfirm,roleResult.Errors.ToList())
                };
            }
            return new CreateUserCommandResponse{
                Result = new Result(ResultStatus.Error, Messages.UserNotAdded,createResult.Errors.ToList())
            };
        }
        return new CreateUserCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.UserNotAdded,createResult.Errors.ToList())
        };
    }
}