using LogiSphere.Application.Models;
using MediatR;

namespace LogiSphere.Application.Features.Vehicles.Commands.RegisterVehicle;

public class RegisterVehicleCommand : IRequest<Result<Guid>>
{
    public string PlateNumber { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public decimal MaxWeightCapacityKg { get; set; }
}
