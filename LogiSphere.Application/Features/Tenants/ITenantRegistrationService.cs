using LogiSphere.Application.Features.Tenants.Models;

namespace LogiSphere.Application.Features.Tenants;

public interface ITenantRegistrationService
{
    public Task<TenantRegistrationResult> HandleTenantRegistrationAsync(TenantRegistrationRequest request);
}
