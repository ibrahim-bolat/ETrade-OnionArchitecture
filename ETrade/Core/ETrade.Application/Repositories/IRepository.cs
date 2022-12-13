using System.Linq.Expressions;
using ETrade.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore.Query;

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
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,bool enableTracking = true);
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,bool enableTracking = true);
    Task<IQueryable<TEntity>> GetAllQueryableAsync(Expression<Func<TEntity, bool>> predicate = null,Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,bool enableTracking = true);
    Task<bool> AnyAsync(Expression<Func<TEntity,bool>> predicate);
    Task<int> CountAsync(Expression<Func<TEntity,bool>> predicate);
}