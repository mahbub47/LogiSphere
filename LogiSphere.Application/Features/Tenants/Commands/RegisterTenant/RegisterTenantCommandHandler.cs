using LogiSphere.Application.Contracts;
using LogiSphere.Application.Core.Abstraction;
using LogiSphere.Application.Core.Errors;
using LogiSphere.Application.Core.Results;
using LogiSphere.Application.Interfaces;
using LogiSphere.Domain.Entities;
using LogiSphere.Domain.Enums;

namespace LogiSphere.Application.Features.Tenants.Commands.RegisterTenant;

internal class RegisterTenantCommandHandler(
    ICatalogUnitOfWork catalogUnitOfWork,
    IIdentityService identityService,
    IEnterpriseProvisioner enterpriceProvisioner) : ICommandHandler<RegisterTenantCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RegisterTenantCommand request, CancellationToken cancellationToken)
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
                ConnectionString = request.Tier == TenantTier.Enterprise ? connectionString : null,
            };

            await catalogUnitOfWork.Tenants.AddAsync(tenant);

            await catalogUnitOfWork.SaveChangesAsync();

            var result = await identityService.CreateTenantAdminAsync(
                tenant.Id,
                request.FullName,
                request.Email,
                request.Phone,
                request.Password);

            if (result.IsFailure)
            {
                await catalogUnitOfWork.RollbackTransactionAsync();
                return Result<Guid>.Failure(UserErrors.UserCreationFailed);
            }

            if (request.Tier == TenantTier.Enterprise)
            {
                var provisionResult = await enterpriceProvisioner.ProvisionTenantDatabaseAsync(
                connectionString);

                if (!provisionResult)
                {
                    await catalogUnitOfWork.RollbackTransactionAsync();
                    return Result<Guid>.Failure(TenantErrors.TenantDatabaseCreationFailed);
                }
            }

            await catalogUnitOfWork.CommitTransactionAsync();
            return Result<Guid>.Success(tenant.Id);
        }
        catch
        {
            await catalogUnitOfWork.RollbackTransactionAsync();
            return Result<Guid>.Failure(TenantErrors.TenantCreationFailed);
        }
    }
}
