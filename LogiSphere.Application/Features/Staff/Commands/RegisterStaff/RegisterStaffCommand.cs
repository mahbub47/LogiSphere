using LogiSphere.Application.Core.Abstraction;
using LogiSphere.Application.Core.Result;
using LogiSphere.Domain.Enums;

namespace LogiSphere.Application.Features.Staff.Commands.RegisterStaff;

public class RegisterStaffCommand : ICommand<Result<Guid>>
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}
