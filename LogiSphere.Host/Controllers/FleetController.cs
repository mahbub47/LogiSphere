using LogiSphere.Application.Features.Vehicles;
using LogiSphere.Application.Features.Vehicles.Models;
using LogiSphere.Host.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogiSphere.Host.Controllers;

[ApiController]
[Route("api/fleet")]
[Authorize(Roles = "FleetManager")]
public class FleetController(IVehicleService service) : ControllerBase
{
    [HttpPost("register-vehicle")]
    public async Task<ActionResult> RegisterVehicle(RegisterVehicleDto request)
    {
        var registrationRequest = new VehicleRegistrationRequest
        {
            PlateNumber = request.PlateNumber,
            Model = request.Model,
            MaxWeightCapacityKg = request.MaxWeightCapacityKg,
        };

        var result = await service.RegisterVehicleAsync(registrationRequest);
        if(result.IsSuccess)
        {
            return Ok(new { Message = "Vehicle registered successfully", VehicleId = result.VehicleId });
        }
        else
        {
            return BadRequest(new { Message = result.Error });
        }
    }

    [HttpGet("vehicles")]
    public async Task<ActionResult> GetAllVehicles()
    {
        var vehicles = await service.GetAllVehicleAsync();
        if(!vehicles.Any())
        {
            return NotFound(new { Message = "No vehicles found" });
        }
        return Ok(vehicles);
    }
}
