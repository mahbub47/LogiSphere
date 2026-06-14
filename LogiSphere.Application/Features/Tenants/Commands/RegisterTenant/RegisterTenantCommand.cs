using LogiSphere.Application.Core.Abstraction;
using LogiSphere.Application.Core.Result;
using LogiSphere.Domain.Enums;

namespace LogiSphere.Application.Features.Tenants.Commands.RegisterTenant;

public class RegisterTenantCommand : ICommand<Result<Guid>>
{
    public string OrganizationName { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public TenantTier Tier { get; set; }

    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}
