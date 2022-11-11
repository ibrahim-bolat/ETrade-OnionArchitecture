using ETrade.Application.Repositories;
using ETrade.Domain.Entities;
using ETrade.Persistence.Contexts;
using Action = ETrade.Domain.Entities.Action;

namespace ETrade.Persistence.Repositories;

public class EfRequestInfoLogRepository:EfGenericRepository<RequestInfoLog>,IRequestInfoLogRepository
{
    public EfRequestInfoLogRepository(DataContext context) : base(context)
    {
        
    }
}