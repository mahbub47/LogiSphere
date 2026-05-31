using LogiSphere.Domain.Enums;

namespace LogiSphere.Application.Features.Staff.Models;

public class StaffRegistrationRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}
