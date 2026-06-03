using LogiSphere.Application.Features.Vehicles.Models;

namespace LogiSphere.Application.Features.Vehicles;

public interface IVehicleService
{
    Task<VehicleRegistrationResult> RegisterVehicleAsync(VehicleRegistrationRequest request);
    Task<IEnumerable<VehicleResponseDto>> GetAllVehicleAsync();
}
