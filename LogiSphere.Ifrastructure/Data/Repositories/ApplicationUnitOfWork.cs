using LogiSphere.Application.Contracts;
using LogiSphere.Application.Interfaces;
using LogiSphere.Domain.Entities;
using LogiSphere.Infrastructure.Data.Database.Context;

namespace LogiSphere.Infrastructure.Data.Repositories;

public class ApplicationUnitOfWork(ApplicationDbContext context) : IApplicationUnitOfWork
{
    private IRepository<Vehicle>? _vehicleRepository;
    private IRepository<DeliveryStop>? _deliveryStopRepository;
    private IRepository<DeliveryRoute>? _deliveryRouteRepository;

    public IRepository<Vehicle> Vehicles => 
        _vehicleRepository ??= new Repository<Vehicle, ApplicationDbContext>(context);

    public IRepository<DeliveryStop> DeliveryStops => 
        _deliveryStopRepository ??= new Repository<DeliveryStop, ApplicationDbContext>(context);

    public IRepository<DeliveryRoute> DeliveryRoutes => 
        _deliveryRouteRepository ??= new Repository<DeliveryRoute, ApplicationDbContext>(context);

    public async Task BeginTransactionAsync() =>
        await context.Database.BeginTransactionAsync();

    public async Task CommitTransactionAsync() =>
        await context.Database.CommitTransactionAsync();

    public async Task RollbackTransactionAsync() =>
        await context.Database.RollbackTransactionAsync();

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await context.SaveChangesAsync(cancellationToken);
    
}
