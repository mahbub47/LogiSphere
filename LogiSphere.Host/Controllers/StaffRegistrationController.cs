using LogiSphere.Application.Features.Staff;
using LogiSphere.Application.Features.Staff.Models;
using LogiSphere.Host.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogiSphere.Host.Controllers;

[ApiController]
[Route("api/staff-registration")]
[Authorize(Roles = "FleetManager")]
public class StaffRegistrationController(IStaffRegistrationService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> RegisterStaff(StaffRegistrationDto request)
    {
        var registerRequest = new StaffRegistrationRequest
        {
            FullName = request.FullName,
            Email = request.Email,
            Password = request.Password,
            Phone = request.Phone,
            Role = request.Role,
        };

        var result = await service.RegisterStaffAsync(registerRequest);

        if (result.IsFailure) return BadRequest();

        return Ok(new { email = request.Email, password = request.Password});
    }
}
