using LogiSphere.Domain.Entities;

namespace LogiSphere.Application.Interfaces;

public interface ICatalogUnitOfWork : IDisposable
{
    IRepository<Tenant> Tenants { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
