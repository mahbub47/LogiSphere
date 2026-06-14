
using LogiSphere.Application.Core.Errors;

namespace LogiSphere.Application.Features.Tenants;

public static class TenantErrors
{
    public static readonly Error TenantNotFound = new("Tenant.TenantNotFound", "Tenant not found.");
    public static readonly Error TenantCreationFailed = new("Tenant.TenantCreationFailed", "Failed to create tenant.");
    public static readonly Error TenantDatabaseCreationFailed = new("Tenant.TenantDatabaseCreationFailed", "Failed to create tenant database.");
}
