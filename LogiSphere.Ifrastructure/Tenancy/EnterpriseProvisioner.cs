using LogiSphere.Application.Contracts;
using LogiSphere.Infrastructure.Data.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace LogiSphere.Infrastructure.Tenancy;

public class EnterpriseProvisioner() : IEnterpriseProvisioner
{
    public async Task<bool> ProvisionTenantDatabaseAsync(string dbConnectionString)
    {
        try
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseNpgsql(dbConnectionString);

            using var tenantDbContext = new ApplicationDbContext(optionBuilder.Options)
                ?? throw new Exception("Database context creation failed");

            await tenantDbContext.Database.MigrateAsync();

            return true;
        }
        catch
        {
            throw new Exception("Database opreration failed");
        }
    }
}
