using LogiSphere.Application.Interfaces;
using LogiSphere.Domain.Entities;

namespace LogiSphere.Application.Contracts;

public interface IApplicationUnitOfWork
{
    IRepository<Vehicle> Vehicles { get; }
    IRepository<DeliveryStop> DeliveryStops { get; }
    IRepository<DeliveryRoute> DeliveryRoutes { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
