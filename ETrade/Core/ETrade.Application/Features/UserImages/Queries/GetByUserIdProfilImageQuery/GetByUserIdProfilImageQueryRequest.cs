using MediatR;

namespace ETrade.Application.Features.UserImages.Queries.GetByUserIdProfilImageQuery;

public class GetByUserIdProfilImageQueryRequest:IRequest<GetByUserIdProfilImageQueryResponse>
{
    public int UserId { get; set; }
}