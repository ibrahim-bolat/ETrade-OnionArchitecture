using ETrade.Application.Repositories;
using ETrade.Domain.Entities;
using ETrade.Persistence.Contexts;

namespace ETrade.Persistence.Repositories;

public class EfMenuRepository:EfGenericRepository<Menu>,IMenuRepository
{
    public EfMenuRepository(DataContext context) : base(context)
    {
        
    }
}