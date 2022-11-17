using System.Linq.Expressions;
using ETrade.Application.Repositories;
using ETrade.Domain.Entities.Common;
using ETrade.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
{
    private readonly DbSet<TEntity> _dbSet;
    
    public Repository(DataContext dbContext)
    {
        _dbSet = dbContext.Set<TEntity>();
    }
    
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        await Task.Run(() =>
        {
            _dbSet.Update(entity);
        });
        return entity;
    }

    public async Task RemoveAsync(TEntity entity)
    {
        await Task.Run(() =>
        {
            _dbSet.Remove(entity);
        });
    }

    public async Task<bool> AddRangeAsync(List<TEntity> entityList)
    {
        await _dbSet.AddRangeAsync(entityList);
        return true;
    }

    public async Task<bool> UpdateRangeAsync(List<TEntity> entityList)
    {
        await Task.Run(() =>
        {
            _dbSet.UpdateRange(entityList);
        });
        return true;
    }

    public async Task<bool> RemoveRangeAsync(List<TEntity> entityList)
    {
        await Task.Run(() =>
        {
            _dbSet.RemoveRange(entityList);
        });
        return true;
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet;
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (includeProperties.Any())
        {
            foreach (var item in includeProperties)
            {
                query = query.Include(item);
            }
        }
        return await query.FirstOrDefaultAsync();
    }

    public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet;
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (includeProperties.Any())
        {
            foreach (var item in includeProperties)
            {
                query = query.Include(item);
            }
        }

        return await query.ToListAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.CountAsync(predicate);
    }
}