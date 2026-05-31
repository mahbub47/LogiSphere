using LogiSphere.Application.Features.Tenants.Models;
using LogiSphere.Host.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LogiSphere.Domain.Enums;
using LogiSphere.Application.Features.Tenants;

namespace LogiSphere.Host.Controllers;

[ApiController]
[Route("api/auth")]
[AllowAnonymous]
public class TenantRegistrationController(ITenantRegistrationService registrationService) : ControllerBase
{
    [HttpPost("tenant-registration")]
    public async Task<ActionResult<TenantRegistrationResponseDto>> RegisterTenant(TenantRegistrationRequestDto request)
    {
        var registration = new TenantRegistrationRequest
        {
            OrganizationName = request.OrganizationName,
            Slug = request.Slug,
            Tier = request.Tier,

            FullName = request.FullName,
            Email = request.Email,
            Phone = request.Phone,
            Password = request.Password,
        };
        var result = await registrationService.HandleTenantRegistrationAsync(registration);

        if (result.IsSuccess)
        {
            var response = new TenantRegistrationResponseDto
            {
                TenantId = result.TenantId,
                Message = $"You successfully registered to the LogiSphere. Your Tenant ID is {result.TenantId}"
            };

            return Created($"api/dashboard/{response.TenantId}", response);
        }

        return BadRequest();
    }
}
