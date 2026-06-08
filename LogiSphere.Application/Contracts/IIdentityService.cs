
using LogiSphere.Domain.Enums;

namespace LogiSphere.Application.Interfaces;

public interface IIdentityService
{
    Task<Guid> CreateTenantAdminAsync(Guid tenantId, string fullname, string email, string phone, string password);
    Task<Guid> CreateStaffAsync(UserRole role, string fullname, string email, string phone, string password);
    Task<(bool, string)> AuthenticateAsync(string email, string password);
}
