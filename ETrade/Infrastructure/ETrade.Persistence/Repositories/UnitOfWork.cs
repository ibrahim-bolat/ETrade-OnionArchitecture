using ETrade.Application.Repositories;
using ETrade.Domain.Entities.Common;
using ETrade.Persistence.Contexts;

namespace ETrade.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dataContext;
    private readonly IServiceProvider _serviceProvider;
    
    public UnitOfWork(DataContext dbContext,IServiceProvider serviceProvider)
    {
        _dataContext = dbContext;;
        _serviceProvider = serviceProvider;
    }
    
    public async ValueTask DisposeAsync()
    {
        await _dataContext.DisposeAsync();
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity, new()
    {
        return (IRepository<TEntity>)_serviceProvider.GetService(typeof(IRepository<TEntity>));
    }

    public async Task<int> SaveAsync()
    {
        return await _dataContext.SaveChangesAsync();
    }
}