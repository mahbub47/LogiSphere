using LogiSphere.Application.Features.Vehicles.Models;
using LogiSphere.Application.Models;
using MediatR;

namespace LogiSphere.Application.Features.Vehicles.Queries.GetAllVehicle;

public class GetAllVehicleQuery : IRequest<Result<IEnumerable<VehicleResponseDto>>>
{
}
