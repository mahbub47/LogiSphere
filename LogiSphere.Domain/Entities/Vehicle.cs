using LogiSphere.Domain.Interfaces;

namespace LogiSphere.Domain.Entities;

public class Vehicle : ITenantable
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string PlateNumber { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public Decimal MaxWeightCapacityKg { get; set; }
    public bool IsActive { get; set; }
}
