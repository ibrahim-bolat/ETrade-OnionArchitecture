using ETrade.Application.Repositories;
using ETrade.Application.Wrappers.Concrete;
using ETrade.Domain.Entities;
using ETrade.Domain.Enums;
using MediatR;

namespace ETrade.Application.Features.UserImages.Queries.GetByUserIdUserImageCountQuery;

public class GetByUserIdUserImageCountQueryHandler:IRequestHandler<GetByUserIdUserImageCountQueryRequest,GetByUserIdUserImageCountQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetByUserIdUserImageCountQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetByUserIdUserImageCountQueryResponse> Handle(GetByUserIdUserImageCountQueryRequest request, CancellationToken cancellationToken)
    {
        var count = await _unitOfWork.GetRepository<UserImage>().CountAsync(x => x.UserId == request.UserId && x.IsActive);
        return new GetByUserIdUserImageCountQueryResponse{
            Result = new DataResult<int>(ResultStatus.Success,count)
        };
    }
}