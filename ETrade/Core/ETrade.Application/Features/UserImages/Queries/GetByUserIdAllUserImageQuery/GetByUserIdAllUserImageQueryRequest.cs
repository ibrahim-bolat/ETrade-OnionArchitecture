using MediatR;

namespace ETrade.Application.Features.UserImages.Queries.GetByUserIdAllUserImageQuery;

public class GetByUserIdAllUserImageQueryRequest:IRequest<GetByUserIdAllUserImageQueryResponse>
{
    public int UserId { get; set; }
}