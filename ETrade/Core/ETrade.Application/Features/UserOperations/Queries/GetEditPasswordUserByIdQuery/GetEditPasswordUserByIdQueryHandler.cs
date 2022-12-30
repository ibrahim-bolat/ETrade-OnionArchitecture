using AutoMapper;
using ETrade.Application.Constants;
using ETrade.Application.Features.UserOperations.DTOs;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities.Identity;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Features.UserOperations.Queries.GetEditPasswordUserByIdQuery;

public class GetEditPasswordUserByIdQueryHandler:IRequestHandler<GetEditPasswordUserByIdQueryRequest,GetEditPasswordUserByIdQueryResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public GetEditPasswordUserByIdQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<GetEditPasswordUserByIdQueryResponse> Handle(GetEditPasswordUserByIdQueryRequest request, CancellationToken cancellationToken)
    {
        AppUser user = await _userManager.FindByIdAsync(request.Id);
        if (user != null)
        {
            if (user.IsActive)
            {
                EditPasswordUserDto editPasswordAccountDto = _mapper.Map<EditPasswordUserDto>(user);
                return new GetEditPasswordUserByIdQueryResponse
                {
                    Result = new DataResult<EditPasswordUserDto>(ResultStatus.Success,editPasswordAccountDto)
                };
            }
            return new GetEditPasswordUserByIdQueryResponse{
                Result = new DataResult<EditPasswordUserDto>(ResultStatus.Error, Messages.UserNotActive,null)
            };
        }
        return new GetEditPasswordUserByIdQueryResponse{
            Result = new DataResult<EditPasswordUserDto>(ResultStatus.Error, Messages.UserNotFound,null)
        };
    }
}