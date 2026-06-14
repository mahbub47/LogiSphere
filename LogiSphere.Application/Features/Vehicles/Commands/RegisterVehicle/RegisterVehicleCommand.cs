using LogiSphere.Application.Core.Abstraction;
using LogiSphere.Application.Core.Result;
using MediatR;

namespace LogiSphere.Application.Features.Vehicles.Commands.RegisterVehicle;

public class RegisterVehicleCommand : ICommand<Result<Guid>>
{
    public string PlateNumber { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public decimal MaxWeightCapacityKg { get; set; }
}
