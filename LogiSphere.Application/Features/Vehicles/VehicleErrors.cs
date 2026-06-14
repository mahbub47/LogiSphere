
using LogiSphere.Application.Core.Errors;

namespace LogiSphere.Application.Features.Vehicles;

public static class VehicleErrors
{
    public static readonly Error VehicleRegistrationFailed = new("Vehicle.VehicleRegistrationFailed", "Failed to register vehicle.");
    public static readonly Error VehicleNotFound = new("Vehicle.VehicleNotFound", "No vehicles found.");
    public static Error VehicleIdNotFound(Guid id) => new("Vehicle.VehicleNotFound", $"Vehicle with ID {id} not found.");
}
