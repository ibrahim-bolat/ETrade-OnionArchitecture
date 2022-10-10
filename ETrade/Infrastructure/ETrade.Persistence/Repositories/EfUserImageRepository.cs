using ETrade.Application.Repositories;
using ETrade.Domain.Entities;
using ETrade.Persistence.Contexts;

namespace ETrade.Persistence.Repositories;

public class EfUserImageRepository:EfGenericRepository<UserImage>,IUserImageRepository
{
    public EfUserImageRepository(DataContext context) : base(context)
    {
    }
}