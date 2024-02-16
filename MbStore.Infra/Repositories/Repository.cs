using MbStore.Domain.Entities.Base;
using MbStore.Infra.Context;
using MbStore.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MbStore.Infra.Repositories;

public class Repository<T> : IRepository<T> where T : EntityBase
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    virtual public async Task<IEnumerable<T>> GetAsync()
    {
        try
        {
            return await _dbSet.ToListAsync();
        }
        catch (Exception ex)
        {
             throw new Exception(ex.Message);
        }
    }

    virtual public async Task<T> GetAsync(Guid id)
    {
        try
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null)
                throw new Exception($"{typeof(T).Name} not found.");

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<T> CreateAsync(T entity)
    {
        try
        {
            entity.CreatedAt = DateTime.Now;
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
             throw new Exception(ex.Message);
        }
    }

    public async Task<T> UpdateAsync(T entity)
    {
        try
        {
            var oldEntity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (oldEntity == null)
                throw new Exception($"Item not found.");

            entity.CreatedAt = oldEntity.CreatedAt;
            entity.UpdatedAt = DateTime.Now;

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
             throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var result = await GetAsync(id);
            if (result == null)
                return false;

            _dbSet.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
             throw new Exception(ex.Message);
        }
    }
}
