namespace LogiSphere.Infrastructure.Interfaces;

public interface ITenantResolver
{
    string? GetConnectionString();
    Guid? GetTenantId();
}
