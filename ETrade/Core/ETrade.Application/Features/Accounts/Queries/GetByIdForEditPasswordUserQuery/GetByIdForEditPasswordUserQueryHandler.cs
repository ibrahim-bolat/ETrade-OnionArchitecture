using AutoMapper;
using ETrade.Application.Features.Accounts.Constants;
using ETrade.Application.Features.Accounts.DTOs;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.Accounts.Queries.GetByIdForEditPasswordUserQuery;

public class GetByIdForEditPasswordUserQueryHandler:IRequestHandler<GetByIdForEditPasswordUserQueryRequest,GetByIdForEditPasswordUserQueryResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public GetByIdForEditPasswordUserQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<GetByIdForEditPasswordUserQueryResponse> Handle(GetByIdForEditPasswordUserQueryRequest request, CancellationToken cancellationToken)
    {
        AppUser user = await _userManager.FindByIdAsync(request.Id);
        if (user != null)
        {
            if (user.IsActive)
            {
                EditPasswordDto editPasswordDto = _mapper.Map<EditPasswordDto>(user);
                return new GetByIdForEditPasswordUserQueryResponse
                {
                    Result = new DataResult<EditPasswordDto>(ResultStatus.Success,editPasswordDto)
                };
            }
            return new GetByIdForEditPasswordUserQueryResponse{
                Result = new DataResult<EditPasswordDto>(ResultStatus.Error, Messages.UserNotActive,null)
            };
        }
        return new GetByIdForEditPasswordUserQueryResponse{
            Result = new DataResult<EditPasswordDto>(ResultStatus.Error, Messages.UserNotFound,null)
        };
    }
}