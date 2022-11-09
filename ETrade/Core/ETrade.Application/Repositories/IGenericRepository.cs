using System.Linq.Expressions;
using ETrade.Domain.Entities.Common;

namespace ETrade.Application.Repositories;

public interface IGenericRepository<T> where T:class,IEntity,new()
{
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task RemoveAsync(T entity);
    Task<bool> AddRangeAsync(List<T> entityList);
    Task<bool> UpdateRangeAsync(List<T> entityList);
    Task<bool> RemoveRangeAsync(List<T> entityList);
    Task<T> GetByIdAsync(int id);
    Task<T> GetAsync(Expression<Func<T,bool>> predicate, params Expression<Func<T,object>>[] includeProperties);
    Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
    Task<bool> AnyAsync(Expression<Func<T,bool>> predicate);
    Task<int> CountAsync(Expression<Func<T,bool>> predicate);
}