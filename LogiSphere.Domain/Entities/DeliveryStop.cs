using LogiSphere.Domain.Enums;

namespace LogiSphere.Domain.Entities;

public class DeliveryStop
{
    public Guid Id { get; set; }
    public Guid RouteId { get; set; }
    public int SequenceOrder { get; set; }
    public string DestinationAddress { get; set; } = string.Empty;
    public Decimal CargoWeightKg { get; set; }
    public DeliveryStatus Status { get; set; }
}
