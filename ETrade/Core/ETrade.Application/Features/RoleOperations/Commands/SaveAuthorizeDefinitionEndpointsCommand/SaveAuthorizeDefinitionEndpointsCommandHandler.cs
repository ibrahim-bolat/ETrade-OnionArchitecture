using ETrade.Application.Constants;
using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Enums;
using MediatR;

namespace ETrade.Application.Features.RoleOperations.Commands.SaveAuthorizeDefinitionEndpointsCommand;

public class SaveAuthorizeDefinitionEndpointsCommandHandler : IRequestHandler<SaveAuthorizeDefinitionEndpointsCommandRequest, SaveAuthorizeDefinitionEndpointsCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public SaveAuthorizeDefinitionEndpointsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SaveAuthorizeDefinitionEndpointsCommandResponse> Handle(SaveAuthorizeDefinitionEndpointsCommandRequest request,
        CancellationToken cancellationToken)
    {
        var menus = await _unitOfWork.MenuRepository.GetAllAsync(m => m.IsActive, m => m.Actions);
        if (menus != null)
        {
            if (request.CheckedIds == null)
            {
                request.CheckedIds  = new List<int>();
            }
            foreach (var menu in menus)
            {
                menu.Checked = request.CheckedIds.Contains(menu.Id);
                await _unitOfWork.MenuRepository.AddAsync(menu);
            }
            await _unitOfWork.SaveAsync();
            return new SaveAuthorizeDefinitionEndpointsCommandResponse
            {
                Result = new Result(ResultStatus.Success, Messages.RoleAdded)
            };
        }
        return new SaveAuthorizeDefinitionEndpointsCommandResponse
        {
            Result = new Result(ResultStatus.Error, Messages.RoleNotAdded)
        };
    }
}