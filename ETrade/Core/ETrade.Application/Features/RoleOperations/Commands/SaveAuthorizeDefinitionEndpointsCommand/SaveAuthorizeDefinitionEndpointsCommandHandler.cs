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
            foreach (var menu in menus)
            {
                int allCheched = 0;
                if (menu.Actions != null)
                {
                    foreach (var action in menu.Actions)
                    {
                        action.Checked = request.CheckedIds.Contains(action.Id);
                        if (action.Checked)
                        {
                            allCheched += 1;
                        }
                    }
                    if (menu.Actions.Count==allCheched)
                    {
                        menu.Checked = true;
                    }
                    else
                    {
                        menu.Checked = false;
                    }
                }
                await _unitOfWork.MenuRepository.UpdateAsync(menu);
            }
            int result = await _unitOfWork.SaveAsync();
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