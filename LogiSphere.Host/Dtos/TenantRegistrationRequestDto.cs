
using LogiSphere.Domain.Enums;

namespace LogiSphere.Host.Dtos;

public class TenantRegistrationRequestDto
{
    public string OrganizationName { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public TenantTier Tier { get; set; }

    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}
