using LogiSphere.Domain.Entities;

namespace LogiSphere.Application.Contracts;

public interface IEnterpriseProvisioner
{
    Task<bool> ProvisionTenantDatabaseAsync(string dbConnectionString);
}
