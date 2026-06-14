using LogiSphere.Domain.Enums;
using LogiSphere.Domain.Interfaces;

namespace LogiSphere.Domain.Entities;

public class DeliveryRoute : ITenantable
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Guid VehicleId { get; set; }
    public Guid DriverId { get; set; }
    public RouteStatus Status { get; set; }

    public Vehicle? Vehicle { get; set; }
    public ICollection<DeliveryStop>? DeliveryStops { get; set; } = new List<DeliveryStop>();
}
