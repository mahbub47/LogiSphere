using LogiSphere.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace LogiSphere.Infrastructure.Tenancy;

public class TenantResolver(IHttpContextAccessor httpContextAccessor, IConfiguration config) : ITenantResolver
{
    public string? GetConnectionString()
    {
        var slugClaim = httpContextAccessor.HttpContext?.User.FindFirst("tenant_slug")?.Value;
        var slug = slugClaim?.ToString();
        if (slug == null) return config.GetConnectionString("sharedDbConnectionString");
        var dbName = $"logisphere_tenant_{slug}";
        var connectionString = $"Host=localhost;Port=5432;Username=postgres;Password=MyPGServer;Database={dbName}";
        return connectionString;
    }

    public Guid? GetTenantId()
    {
        var tenantIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(tenantIdClaim, out var tenantId) ? tenantId : null;
    }
}
