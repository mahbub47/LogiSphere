namespace LogiSphere.Host.Dtos;

public class RegisterVehicleDto
{
    public string PlateNumber { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public decimal MaxWeightCapacityKg { get; set; }
}
