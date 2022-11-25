using AutoMapper;
using ETrade.Application.Features.IpOperations.Constants;
using ETrade.Application.Features.IpOperations.DTOs;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ETrade.Application.Features.IpOperations.Commands.UpdateIpAddressCommand;

public class UpdateIpAddressCommandHandler : IRequestHandler<UpdateIpAddressCommandRequest, UpdateIpAddressCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public UpdateIpAddressCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<UpdateIpAddressCommandResponse> Handle(UpdateIpAddressCommandRequest request,
        CancellationToken cancellationToken)
    {
        IpAddress ipAddress = await _unitOfWork.GetRepository<IpAddress>().GetByIdAsync(request.IpDto.Id);
        if (ipAddress != null)
        {
            ipAddress= _mapper.Map(request.IpDto,ipAddress);
            ipAddress.ModifiedTime = DateTime.Now;
            ipAddress.ModifiedByName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
            await _unitOfWork.GetRepository<IpAddress>().UpdateAsync(ipAddress);
            int result = await _unitOfWork.SaveAsync();
            if (result > 0)
            {
                return new UpdateIpAddressCommandResponse
                {
                    Result = new DataResult<IpDto>(ResultStatus.Success, Messages.IpUpdated, request.IpDto)
                };
            }
            return new UpdateIpAddressCommandResponse{
                Result = new Result(ResultStatus.Error, Messages.IpNotUpdated)
            };
        }
        return new UpdateIpAddressCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.IpNotFound)
        };
    }
}