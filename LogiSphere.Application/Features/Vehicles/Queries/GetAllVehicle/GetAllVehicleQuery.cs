using LogiSphere.Application.Core.Abstraction;
using LogiSphere.Application.Core.Results;
using LogiSphere.Application.Features.Vehicles.DTOs;

namespace LogiSphere.Application.Features.Vehicles.Queries.GetAllVehicle;

public class GetAllVehicleQuery : IQuery<Result<IEnumerable<VehicleResponseDto>>>
{
}
