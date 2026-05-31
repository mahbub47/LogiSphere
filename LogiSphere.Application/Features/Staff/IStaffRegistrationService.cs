using LogiSphere.Application.Features.Staff.Models;

namespace LogiSphere.Application.Features.Staff;

public interface IStaffRegistrationService
{
    Task<StaffRegistrationResult> RegisterStaffAsync(StaffRegistrationRequest request);
}
