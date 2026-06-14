using LogiSphere.Application.Features.Vehicles.Commands.RegisterVehicle;
using LogiSphere.Application.Features.Vehicles.Queries.GetAllVehicle;
using LogiSphere.Host.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogiSphere.Host.Controllers;

[ApiController]
[Route("api/fleet")]
[Authorize(Roles = "FleetManager")]
public class FleetController(ISender _sender) : ControllerBase
{
    [HttpPost("register-vehicle")]
    public async Task<ActionResult> RegisterVehicle(RegisterVehicleDto request)
    {
        var registrationRequest = new RegisterVehicleCommand
        {
            PlateNumber = request.PlateNumber,
            Model = request.Model,
            MaxWeightCapacityKg = request.MaxWeightCapacityKg,
        };

        var result = await _sender.Send(registrationRequest);
        if(result.IsSuccess)
        {
            return Ok(new { Id = result.Value });
        }
        else
        {
            return BadRequest(new { Message = result.Error });
        }
    }

    [HttpGet("vehicles")]
    public async Task<ActionResult> GetAllVehicles()
    {
        var result = await _sender.Send(new GetAllVehicleQuery());
        if(result.IsFailure)
        {
            return NotFound(new { Message = result.Error.Description });
        }
        return Ok(result.Value);
    }
}
