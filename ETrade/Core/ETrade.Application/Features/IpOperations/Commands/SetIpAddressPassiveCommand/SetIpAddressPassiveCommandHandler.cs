using AutoMapper;
using ETrade.Application.Features.IpOperations.Constants;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ETrade.Application.Features.IpOperations.Commands.SetIpAddressPassiveCommand;

public class SetIpAddressPassiveCommandCommandHandler : IRequestHandler<SetIpAddressPassiveCommandRequest, SetIpAddressPassiveCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper; 
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SetIpAddressPassiveCommandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<SetIpAddressPassiveCommandResponse> Handle(SetIpAddressPassiveCommandRequest request,
        CancellationToken cancellationToken)
    {
        IpAddress ipAddress = await _unitOfWork.GetRepository<IpAddress>().GetByIdAsync(request.Id);
        if (ipAddress != null)
        {
            if (ipAddress.IsActive)
            {
                ipAddress.IsActive = false;
                ipAddress.IsDeleted = true;
                ipAddress.ModifiedTime = DateTime.Now;
                ipAddress.ModifiedByName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
                await _unitOfWork.GetRepository<IpAddress>().UpdateAsync(ipAddress);
                int result = await _unitOfWork.SaveAsync();
                if (result > 0)
                {
                    return new SetIpAddressPassiveCommandResponse
                    {
                        Result = new  Result(ResultStatus.Success, Messages.IpUpdated)
                    };
                }
                return new SetIpAddressPassiveCommandResponse{
                    Result = new Result(ResultStatus.Error, Messages.IpNotUpdated)
                };
            }
            return new SetIpAddressPassiveCommandResponse
            {
                Result = new Result(ResultStatus.Error, Messages.IpNotActive)
            };
        }
        return new SetIpAddressPassiveCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.IpNotFound)
        };
    }
}