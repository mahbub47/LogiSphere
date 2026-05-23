
namespace LogiSphere.Application.Interfaces;

public interface IIdentityService
{
    Task<bool> CreateTenantAdminAsync(Guid tenantId, string fullname, string email, string phone, string password);
}
