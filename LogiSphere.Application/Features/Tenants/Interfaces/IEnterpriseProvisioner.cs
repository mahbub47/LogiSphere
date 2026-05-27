using LogiSphere.Domain.Entities;

namespace LogiSphere.Application.Features.Tenants.Interfaces;

public interface IEnterpriseProvisioner
{
    Task<bool> ProvisionTenantDatabaseAsync(string dbConnectionString);
}
