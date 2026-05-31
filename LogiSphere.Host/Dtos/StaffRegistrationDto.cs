using LogiSphere.Domain.Enums;

namespace LogiSphere.Host.Dtos;

public class StaffRegistrationDto
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}
