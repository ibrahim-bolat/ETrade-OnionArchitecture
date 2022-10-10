using ETrade.Application.Repositories;
using ETrade.Domain.Entities.Identity;
using ETrade.Persistence.Contexts;

namespace ETrade.Persistence.Repositories;

public class EfUserRepository:EfGenericRepository<AppUser>,IUserRepository
{
    public EfUserRepository(DataContext context) : base(context)
    {
        
    }
}