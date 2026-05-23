using LogiSphere.Domain.Entities;

namespace LogiSphere.Application.Features.Tenants.Interfaces;

public interface IEnterpriceProvisioner
{
    Task<bool> ProvisionTenantDatabaseAsync(
        string dbConnectionString,
        Tenant tenantId,
        string fullname,
        string email,
        string phone,
        string password);
}
