using ETrade.Application.DTOs;
using ETrade.Application.Features.Accounts.DTOs;
using MediatR;

namespace ETrade.Application.Features.Accounts.Commands.EditPasswordAccountCommand;

public class EditPasswordAccountCommandRequest:IRequest<EditPasswordAccountCommandResponse>
{
    public EditPasswordAccountDto EditPasswordAccountDto { get; set; }
}