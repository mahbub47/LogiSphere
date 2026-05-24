

using LogiSphere.Application.Features.Tenants.Interfaces;
using LogiSphere.Application.Features.Tenants.Models;
using LogiSphere.Application.Interfaces;
using LogiSphere.Domain.Entities;

namespace LogiSphere.Application.Features.Tenants;

public class TenantRegistrationService(
    IRepository<Tenant> tenantRepository,
    IUnitOfWork unitOfWork,
    IIdentityService identityService,
    IEnterpriceProvisioner enterpriceProvisioner) : ITenantRegistrationService

{
    public async Task<TenantRegistrationResult> HandleEnterpriceTenantRegistration(TenantRegistrationRequest request)
    {
        try
        {
            await unitOfWork.BeginTransactionAsync();
            var dbName = $"logisphere_tenant_{request.Slug}";
            var connectionString = $"Host=localhost;Port=5432;Username=postgres;Password=MyPGServer;Database={dbName}";
            var tenant = new Tenant
            {
                Name = request.OrganizationName,
                Slug = request.Slug,
                Tier = request.Tier,
                ConnectionString = connectionString
            };

            await tenantRepository.AddAsync(tenant);

            await unitOfWork.SaveChangesAsync();

            var provisionResult = await enterpriceProvisioner.ProvisionTenantDatabaseAsync(
                connectionString,
                tenant,
                request.FullName,
                request.Email,
                request.Phone,
                request.Password);

            if (!provisionResult)
            {
                await unitOfWork.RollbackTransactionAsync();
                return TenantRegistrationResult.Failed("Database provision for enterprice level organization failed!");
            }

            await unitOfWork.CommitTransactionAsync();
            return TenantRegistrationResult.Succeed(tenant.Id);
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            return TenantRegistrationResult.Failed("Database provision for enterprice level organization failed!");
        }
    }

    public async Task<TenantRegistrationResult> HandleStandardTenantRegistration(TenantRegistrationRequest request)
    {
        try
        {
            await unitOfWork.BeginTransactionAsync();
            var tenant = new Tenant
            {
                Name = request.OrganizationName,
                Slug = request.Slug,
                Tier = request.Tier
            };

            await tenantRepository.AddAsync(tenant);

            await unitOfWork.SaveChangesAsync();

            var result = await identityService.CreateTenantAdminAsync(
                tenant.Id,
                request.FullName,
                request.Email,
                request.Phone,
                request.Password);

            if (!result)
            {
                await unitOfWork.RollbackTransactionAsync();
                return TenantRegistrationResult.Failed("Admin creation failed due to some issue");
            }

            await unitOfWork.CommitTransactionAsync();
            return TenantRegistrationResult.Succeed(tenant.Id);
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            return TenantRegistrationResult.Failed("Transaction failed due to some issue");
        }
    }
}
