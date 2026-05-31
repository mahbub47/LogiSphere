using LogiSphere.Domain.Entities;
using LogiSphere.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LogiSphere.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>, ITenantable
{
    public Guid TenantId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime? RegisteredAt { get; set; }

    public Tenant? Tenant { get; set; }
}
