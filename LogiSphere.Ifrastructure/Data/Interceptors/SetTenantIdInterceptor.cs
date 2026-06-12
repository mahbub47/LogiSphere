using LogiSphere.Domain.Interfaces;
using LogiSphere.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LogiSphere.Infrastructure.Data.Interceptors;

public class SetTenantIdInterceptor(ITenantResolver tenantIdProvider) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, 
        InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result, 
        CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        var tenantId = tenantIdProvider.GetTenantId();
        
        foreach(var entry in context.ChangeTracker.Entries<ITenantable>().Where(e => e.State == EntityState.Added))
        {
            if(entry.Entity.TenantId == Guid.Empty && tenantId.HasValue)
            {
                entry.Entity.TenantId = tenantId ?? throw new ArgumentNullException(nameof(tenantId));
            }
        }
    }
}
