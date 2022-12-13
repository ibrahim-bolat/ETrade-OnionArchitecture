using System.Linq.Expressions;
using ETrade.Application.Repositories;
using ETrade.Domain.Entities.Common;
using ETrade.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,bool enableTracking = true)
    {
        IQueryable<TEntity> query = _dbSet;
        if (!enableTracking)
        {
            query = query.AsNoTracking();
        }
        if (include!=null)
        {
            query = include(query);
        }
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,bool enableTracking = true)
    {
        IQueryable<TEntity> query = _dbSet;
        if (!enableTracking)
        {
            query = query.AsNoTracking();
        }
        if (include!=null)
        {
            query = include(query);
        }
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        if (orderBy != null)
        {
            query = orderBy(query);
        }
        return await query.ToListAsync();
    }
    
    public async Task<IQueryable<TEntity>> GetAllQueryableAsync(Expression<Func<TEntity, bool>> predicate = null,Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,bool enableTracking = true)
    {
        IQueryable<TEntity> query = _dbSet;
        if (!enableTracking)
        {
            query = query.AsNoTracking();
        }
        if (include!=null)
        {
            query = include(query);
        }
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        if (orderBy != null)
        {
            query = orderBy(query);
        }
        return await Task.FromResult(query.AsQueryable());
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