using ETrade.Application.Repositories;
using ETrade.Domain.Entities;
using ETrade.Persistence.Contexts;
using Action = ETrade.Domain.Entities.Action;

namespace ETrade.Persistence.Repositories;

public class EfActionRepository:EfGenericRepository<Action>,IActionRepository
{
    public EfActionRepository(DataContext context) : base(context)
    {
        
    }
}