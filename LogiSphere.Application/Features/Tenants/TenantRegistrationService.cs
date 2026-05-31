

using LogiSphere.Application.Contracts;
using LogiSphere.Application.Features.Tenants.Models;
using LogiSphere.Application.Interfaces;
using LogiSphere.Domain.Entities;
using LogiSphere.Domain.Enums;

namespace LogiSphere.Application.Features.Tenants;

public class TenantRegistrationService(
    ICatalogUnitOfWork catalogUnitOfWork,
    IIdentityService identityService,
    IEnterpriseProvisioner enterpriceProvisioner) : ITenantRegistrationService

{
    public async Task<TenantRegistrationResult> HandleTenantRegistrationAsync(TenantRegistrationRequest request)
    {
        try
        {
            await catalogUnitOfWork.BeginTransactionAsync();

            var dbName = $"logisphere_tenant_{request.Slug}";
            var connectionString = $"Host=localhost;Port=5432;Username=postgres;Password=MyPGServer;Database={dbName}";

            var tenant = new Tenant
            {
                Name = request.OrganizationName,
                Slug = request.Slug,
                Tier = request.Tier,
                ConnectionString = request.Tier == TenantTier.Enterprice ? connectionString : null,
            };

            await catalogUnitOfWork.Tenants.AddAsync(tenant);

            await catalogUnitOfWork.SaveChangesAsync();

            var result = await identityService.CreateTenantAdminAsync(
                tenant.Id,
                request.FullName,
                request.Email,
                request.Phone,
                request.Password);

            if (!result)
            {
                await catalogUnitOfWork.RollbackTransactionAsync();
                return TenantRegistrationResult.Failed("Admin creation failed due to some issue");
            }

            if(request.Tier == TenantTier.Enterprice)
            {
                var provisionResult = await enterpriceProvisioner.ProvisionTenantDatabaseAsync(
                connectionString);

                if (!provisionResult)
                {
                    await catalogUnitOfWork.RollbackTransactionAsync();
                    return TenantRegistrationResult.Failed("Database provision for enterprice level organization failed!");
                }
            }

            await catalogUnitOfWork.CommitTransactionAsync();
            return TenantRegistrationResult.Succeed(tenant.Id);
        }
        catch
        {
            await catalogUnitOfWork.RollbackTransactionAsync();
            return TenantRegistrationResult.Failed("Database provision for enterprice level organization failed!");
        }
    }
}
