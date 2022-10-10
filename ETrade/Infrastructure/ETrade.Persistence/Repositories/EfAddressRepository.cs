using ETrade.Application.Repositories;
using ETrade.Domain.Entities;
using ETrade.Persistence.Contexts;

namespace ETrade.Persistence.Repositories;

public class EfAddressRepository:EfGenericRepository<Address>,IAddressRepository
{
    public EfAddressRepository(DataContext context) : base(context)
    {
        
    }
}