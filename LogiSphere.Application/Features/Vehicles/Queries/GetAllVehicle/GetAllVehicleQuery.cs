using LogiSphere.Application.Core.Result;
using LogiSphere.Application.DTOs;
using MediatR;

namespace LogiSphere.Application.Features.Vehicles.Queries.GetAllVehicle;

public class GetAllVehicleQuery : IRequest<Result<IEnumerable<VehicleResponseDto>>>
{
}
