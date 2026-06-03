using LogiSphere.Domain.Enums;
using LogiSphere.Domain.Interfaces;

namespace LogiSphere.Domain.Entities;

public class DeliveryStop : ITenantable
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Guid RouteId { get; set; }
    public int SequenceOrder { get; set; }
    public string DestinationAddress { get; set; } = string.Empty;
    public Decimal CargoWeightKg { get; set; }
    public DeliveryStatus Status { get; set; }

    public DeliveryRoute? Route { get; set; }
}
