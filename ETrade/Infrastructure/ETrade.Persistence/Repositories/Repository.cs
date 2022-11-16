using System.Linq.Expressions;
using ETrade.Application.Repositories;
using ETrade.Domain.Entities;
using ETrade.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Action = ETrade.Domain.Entities.Action;

namespace ETrade.Persistence.Repositories;

public class Repository<TEntity>:IRepository<TEntity> where TEntity:class,IEntity,new()
{
    protected readonly DbContext _context;
    

    public Repository(DbContext context)
    {
        _context = context;
    }
    
    private DbSet<TEntity> Table { get => _context.Set<TEntity>(); }
    
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await Table.AddAsync(entity);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        await Task.Run(() =>
        {
            Table.Update(entity);
        });
        return entity;
    }

    public async Task RemoveAsync(TEntity entity)
    {
        await Task.Run(() =>
        {
            Table.Remove(entity);
        });
    }

    public async Task<bool> AddRangeAsync(List<TEntity> entityList)
    {
        await Table.AddRangeAsync(entityList);
        return true;
    }

    public async Task<bool> UpdateRangeAsync(List<TEntity> entityList)
    {
        await Task.Run(() =>
        {
            Table.UpdateRange(entityList);
        });
        return true;
    }

    public async Task<bool> RemoveRangeAsync(List<TEntity> entityList)
    {
        await Task.Run(() =>
        {
            Table.RemoveRange(entityList);
        });
        return true;
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await Table.FindAsync(id);
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = Table;
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
        IQueryable<TEntity> query = Table;
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
        return await Table.AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Table.CountAsync(predicate);
    }
}