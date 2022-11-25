using System.Linq.Expressions;
using ETrade.Domain.Entities.Common;

namespace ETrade.Application.Repositories;

public interface IRepository<TEntity> where TEntity : class, IEntity, new()
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
    Task<bool> AddRangeAsync(List<TEntity> entityList);
    Task<bool> UpdateRangeAsync(List<TEntity> entityList);
    Task<bool> RemoveRangeAsync(List<TEntity> entityList);
    Task<TEntity> GetByIdAsync(int id);
    Task<TEntity> GetAsync(Expression<Func<TEntity,bool>> predicate, params Expression<Func<TEntity,object>>[] includeProperties);
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties);
    Task<IQueryable<TEntity>> GetAllQueryableAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties);
    Task<bool> AnyAsync(Expression<Func<TEntity,bool>> predicate);
    Task<int> CountAsync(Expression<Func<TEntity,bool>> predicate);
}