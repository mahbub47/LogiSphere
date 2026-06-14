using LogiSphere.Application.Core.Errors;

namespace LogiSphere.Application.Features.Staff;

public static class StaffErrors
{
    public static readonly Error StaffNotFound = new("Staff.StaffNotFound", "Staff not found.");
    public static readonly Error StaffCreationFailed = new("Staff.StaffCreationFailed", "Staff creation failed.");
}
