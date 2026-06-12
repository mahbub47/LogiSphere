using LogiSphere.Application.Interfaces;
using LogiSphere.Domain.Entities;
using LogiSphere.Infrastructure.Data.Database.Context;

namespace LogiSphere.Infrastructure.Data.Repositories;

public class CatalogUnitOfWork(CatalogDbContext _context) : ICatalogUnitOfWork
{
    private IRepository<Tenant>? _tenants;
    public IRepository<Tenant> Tenants => 
        _tenants ??= new Repository<Tenant, CatalogDbContext>(_context);

    public async Task BeginTransactionAsync() => 
        await _context.Database.BeginTransactionAsync();

    public async Task CommitTransactionAsync() => 
        await _context.Database.CommitTransactionAsync();

    public void Dispose() => _context.Dispose();

    public async Task RollbackTransactionAsync() => 
        await _context.Database.RollbackTransactionAsync();

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => 
        await _context.SaveChangesAsync(cancellationToken);
}
