using LogiSphere.Application.Features.Tenants.Commands.RegisterTenant;
using LogiSphere.Host.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogiSphere.Host.Controllers;

[ApiController]
[Route("api/auth")]
[AllowAnonymous]
public class TenantRegistrationController(ISender _sender) : ControllerBase
{
    [HttpPost("tenant-registration")]
    public async Task<ActionResult<TenantRegistrationResponseDto>> RegisterTenant(TenantRegistrationRequestDto request)
    {
        var registration = new RegisterTenantCommand
        {
            OrganizationName = request.OrganizationName,
            Slug = request.Slug,
            Tier = request.Tier,

            FullName = request.FullName,
            Email = request.Email,
            Phone = request.Phone,
            Password = request.Password,
        };
        var result = await _sender.Send(registration);

        if (result.IsSuccess)
        {
            var response = new TenantRegistrationResponseDto
            {
                TenantId = result.Value,
                Message = $"You successfully registered to the LogiSphere. Your Tenant ID is {result.Value}"
            };

            return Created($"api/dashboard/{response.TenantId}", response);
        }

        return BadRequest();
    }
}
