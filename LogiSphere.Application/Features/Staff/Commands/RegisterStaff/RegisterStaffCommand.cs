using LogiSphere.Application.Models;
using LogiSphere.Domain.Enums;
using MediatR;

namespace LogiSphere.Application.Features.Staff.Commands.RegisterStaff;

public class RegisterStaffCommand : IRequest<Result<Guid>>
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}
