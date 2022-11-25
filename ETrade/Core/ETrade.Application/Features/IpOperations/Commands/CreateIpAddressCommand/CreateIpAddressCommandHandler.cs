using AutoMapper;
using ETrade.Application.Features.IpOperations.Constants;
using ETrade.Application.Features.IpOperations.DTOs;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ETrade.Application.Features.IpOperations.Commands.CreateIpAddressCommand;

public class CreateIpAddressCommandHandler : IRequestHandler<CreateIpAddressCommandRequest, CreateIpAddressCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateIpAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CreateIpAddressCommandResponse> Handle(CreateIpAddressCommandRequest request,
        CancellationToken cancellationToken)
    {
        string createdByName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        var newIpAddress = _mapper.Map<IpAddress>(request.IpDto);
        newIpAddress.CreatedByName = createdByName;
        newIpAddress.ModifiedByName = createdByName;
        newIpAddress.CreatedTime = DateTime.Now;
        newIpAddress.ModifiedTime = DateTime.Now;
        newIpAddress.IsActive = true;
        newIpAddress.IsDeleted = false;
        await _unitOfWork.GetRepository<IpAddress>().AddAsync(newIpAddress);
        int result = await _unitOfWork.SaveAsync();
        if (result > 0)
        {
            return new CreateIpAddressCommandResponse
            {
                Result = new DataResult<IpDto>(ResultStatus.Success, Messages.IpAdded, request.IpDto)
            };
        }
        return new CreateIpAddressCommandResponse{
            Result = new Result(ResultStatus.Error, Messages.IpNotAdded)
        };
    }
}