using LogiSphere.Domain.Entities;
using LogiSphere.Infrastructure.Data.Models;
namespace LogiSphere.Infrastructure.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(ApplicationUser user, Tenant tenant, string role);
}
