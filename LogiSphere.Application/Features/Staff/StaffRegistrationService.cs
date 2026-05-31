using LogiSphere.Application.Features.Staff.Models;
using LogiSphere.Application.Interfaces;

namespace LogiSphere.Application.Features.Staff;

public class StaffRegistrationService(IIdentityService identityService) : IStaffRegistrationService
{
    public async Task<StaffRegistrationResult> RegisterStaffAsync(StaffRegistrationRequest request)
    {
        var result = await identityService.CreateStaffAsync(
            request.Role,
            request.FullName,
            request.Email,
            request.Phone,
            request.Password);

        if (!result) return StaffRegistrationResult.Failed("Staff registration failed");

        return StaffRegistrationResult.Success();
    }
}
