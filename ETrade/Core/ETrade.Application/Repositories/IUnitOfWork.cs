using ETrade.Domain.Entities.Common;

namespace ETrade.Application.Repositories;

public interface IUnitOfWork:IAsyncDisposable
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity, new();
    Task<int> SaveAsync();
}