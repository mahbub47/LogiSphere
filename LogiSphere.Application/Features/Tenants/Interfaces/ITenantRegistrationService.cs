using LogiSphere.Application.Features.Tenants.Models;

namespace LogiSphere.Application.Features.Tenants.Interfaces;

public interface ITenantRegistrationService
{
    public Task<TenantRegistrationResult> HandleStandardTenantRegistration(TenantRegistrationRequest request);
    public Task<TenantRegistrationResult> HandleEnterpriceTenantRegistration(TenantRegistrationRequest request);
}
