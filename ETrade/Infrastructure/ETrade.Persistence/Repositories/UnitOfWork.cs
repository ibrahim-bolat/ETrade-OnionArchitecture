
using ETrade.Application.Repositories;
using ETrade.Domain.Entities.Common;
using ETrade.Persistence.Contexts;

namespace ETrade.Persistence.Repositories;

public class UnitOfWork:IUnitOfWork
{
    private readonly DataContext _dataContext;

    
    public UnitOfWork(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async ValueTask DisposeAsync()
    {
        await _dataContext.DisposeAsync();
    }

    IRepository<TEntity> IUnitOfWork.GetRepository<TEntity>()
    {
        return new Repository<TEntity>(_dataContext);
    }

    public async Task<int> SaveAsync()
    {
        return await _dataContext.SaveChangesAsync();
    }
}