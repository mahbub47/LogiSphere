using LogiSphere.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogiSphere.Infrastructure.Data.Repositories;

public class Repository<TEntity, TContext> : IRepository<TEntity> 
    where TEntity : class 
    where TContext : DbContext
{
    private readonly TContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(TContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }
}
