namespace LogiSphere.Application.Features.Vehicles.Models;

public class VehicleResponseDto
{
    public Guid Id { get; set; }
    public string PlateNumber { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public decimal MaxWeightCapacityKg { get; set; }
}
