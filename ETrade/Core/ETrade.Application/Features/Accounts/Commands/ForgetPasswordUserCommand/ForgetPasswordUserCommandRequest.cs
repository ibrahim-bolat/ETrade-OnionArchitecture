using ETrade.Application.Features.Accounts.DTOs.UserDtos;
using MediatR;

namespace ETrade.Application.Features.Accounts.Commands.ForgetPasswordUserCommand;

public class ForgetPasswordUserCommandRequest:IRequest<ForgetPasswordUserCommandResponse>
{
    public ForgetPassDto ForgetPassDto { get; set; }
}