using ETrade.Application.Features.Accounts.DTOs.UserDtos;
using MediatR;

namespace ETrade.Application.Features.Accounts.Queries.ForgetPasswordUserQuery;

public class ForgetPasswordUserQueryRequest:IRequest<ForgetPasswordUserQueryResponse>
{
    public ForgetPassDto ForgetPassDto { get; set; }
}