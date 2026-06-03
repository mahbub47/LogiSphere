namespace LogiSphere.Application.Features.Vehicles.Models;

public class VehicleRegistrationRequest
{
    public string PlateNumber { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public decimal MaxWeightCapacityKg { get; set; }
}
