
using LogiSphere.Application.Core.Result;
using LogiSphere.Domain.Enums;

namespace LogiSphere.Application.Interfaces;

public interface IIdentityService
{
    Task<Result<Guid>> CreateTenantAdminAsync(Guid tenantId, string fullname, string email, string phone, string password);
    Task<Result<Guid>> CreateStaffAsync(UserRole role, string fullname, string email, string phone, string password);
    Task<Result<string>> AuthenticateAsync(string email, string password);
}
