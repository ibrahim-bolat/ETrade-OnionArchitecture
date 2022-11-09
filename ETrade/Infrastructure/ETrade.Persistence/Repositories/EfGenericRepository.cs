using System.Linq.Expressions;
using ETrade.Application.Repositories;
using ETrade.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Persistence.Repositories;

public class EfGenericRepository<TEntity>:IGenericRepository<TEntity> where TEntity:class,IEntity,new()
{
    protected readonly DbContext _context;

    public EfGenericRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        await Task.Run(() =>
        {
            _context.Set<TEntity>().Update(entity);
        });
        return entity;
    }

    public async Task RemoveAsync(TEntity entity)
    {
        await Task.Run(() =>
        {
            _context.Set<TEntity>().Remove(entity);
        });
    }

    public async Task<bool> AddRangeAsync(List<TEntity> entityList)
    {
        await _context.Set<TEntity>().AddRangeAsync(entityList);
        return true;
    }

    public async Task<bool> UpdateRangeAsync(List<TEntity> entityList)
    {
        await Task.Run(() =>
        {
            _context.Set<TEntity>().UpdateRange(entityList);
        });
        return true;
    }

    public async Task<bool> RemoveRangeAsync(List<TEntity> entityList)
    {
        await Task.Run(() =>
        {
            _context.Set<TEntity>().RemoveRange(entityList);
        });
        return true;
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
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
        IQueryable<TEntity> query = _context.Set<TEntity>();
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
        return await _context.Set<TEntity>().AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().CountAsync(predicate);
    }
}