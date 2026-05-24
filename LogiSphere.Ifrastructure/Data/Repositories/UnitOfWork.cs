using LogiSphere.Application.Interfaces;
using LogiSphere.Infrastructure.Data.DbContext;

namespace LogiSphere.Infrastructure.Data.Repositories;

public class UnitOfWork(ApplicationDbContext _context) : IUnitOfWork
{
    public async Task BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();

    public async Task CommitTransactionAsync() => await _context.Database.CommitTransactionAsync();

    public void Dispose() => _context.Dispose();

    public async Task RollbackTransactionAsync() => await _context.Database.RollbackTransactionAsync();

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => 
        await _context.SaveChangesAsync(cancellationToken);
}
