using LogiSphere.Domain.Entities;
using LogiSphere.Infrastructure.Identity;
namespace LogiSphere.Infrastructure.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(ApplicationUser user, Tenant tenant, string role);
}
