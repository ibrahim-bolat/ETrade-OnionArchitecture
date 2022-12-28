using AutoMapper;
using ETrade.Application.Constants;
using ETrade.Application.DTOs;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.Accounts.Queries.GetEditPasswordAccountByIdQuery;

public class GetEditPasswordAccountByIdQueryHandler:IRequestHandler<GetEditPasswordAccountByIdQueryRequest,GetEditPasswordAccountByIdQueryResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetEditPasswordAccountByIdQueryHandler(UserManager<AppUser> userManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GetEditPasswordAccountByIdQueryResponse> Handle(GetEditPasswordAccountByIdQueryRequest request, CancellationToken cancellationToken)
    {
        AppUser user = await _userManager.FindByIdAsync(request.Id);
        string contextUserName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
        if (user.UserName.Equals(contextUserName))
        {
            if (user != null)
            {
                if (user.IsActive)
                {
                    EditPasswordDto editPasswordDto = _mapper.Map<EditPasswordDto>(user);
                    return new GetEditPasswordAccountByIdQueryResponse
                    {
                        Result = new DataResult<EditPasswordDto>(ResultStatus.Success,editPasswordDto)
                    };
                }
                return new GetEditPasswordAccountByIdQueryResponse{
                    Result = new DataResult<EditPasswordDto>(ResultStatus.Error, Messages.UserNotActive,null)
                };
            }
        }
        return new GetEditPasswordAccountByIdQueryResponse{
            Result = new DataResult<EditPasswordDto>(ResultStatus.Error, Messages.UserNotFound,null)
        };
    }
}