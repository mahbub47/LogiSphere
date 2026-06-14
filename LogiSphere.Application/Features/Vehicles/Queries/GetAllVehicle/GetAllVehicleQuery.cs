using LogiSphere.Application.Core.Abstraction;
using LogiSphere.Application.Core.Result;
using LogiSphere.Application.DTOs;

namespace LogiSphere.Application.Features.Vehicles.Queries.GetAllVehicle;

public class GetAllVehicleQuery : IQuery<Result<IEnumerable<VehicleResponseDto>>>
{
}
