using LogiSphere.Application.Features.Staff.Commands.RegisterStaff;
using LogiSphere.Host.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogiSphere.Host.Controllers;

[ApiController]
[Route("api/staff-registration")]
[Authorize(Roles = "FleetManager")]
public class StaffRegistrationController(ISender _sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> RegisterStaff(StaffRegistrationDto request)
    {
        var registerRequest = new RegisterStaffCommand
        {
            FullName = request.FullName,
            Email = request.Email,
            Password = request.Password,
            Phone = request.Phone,
            Role = request.Role,
        };

        var result = await _sender.Send(registerRequest);

        if (result.IsFailure) return BadRequest();

        return Ok(new { Id = result.Value });
    }
}
