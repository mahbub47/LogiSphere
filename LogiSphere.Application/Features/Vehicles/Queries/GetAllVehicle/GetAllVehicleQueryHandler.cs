using LogiSphere.Application.Contracts;
using LogiSphere.Application.Core.Abstraction;
using LogiSphere.Application.Core.Results;
using LogiSphere.Application.Features.Vehicles.DTOs;

namespace LogiSphere.Application.Features.Vehicles.Queries.GetAllVehicle;

internal class GetAllVehicleQueryHandler(IApplicationUnitOfWork unitOfWork) : IQueryHandler<GetAllVehicleQuery, Result<IEnumerable<VehicleResponseDto>>>
{
    public async Task<Result<IEnumerable<VehicleResponseDto>>> Handle(GetAllVehicleQuery request, CancellationToken cancellationToken)
    {
        var vehicles = new List<VehicleResponseDto>();
        var vehicleEntities = await unitOfWork.Vehicles.GetAllAsync();
        if(!vehicleEntities.Any())
        {
            return Result<IEnumerable<VehicleResponseDto>>.Failure(VehicleErrors.VehicleNotFound);
        }
        foreach (var entity in vehicleEntities)
        {
            vehicles.Add(new VehicleResponseDto
            {
                Id = entity.Id,
                PlateNumber = entity.PlateNumber,
                Model = entity.Model,
                MaxWeightCapacityKg = entity.MaxWeightCapacityKg
            });
        }
        return Result<IEnumerable<VehicleResponseDto>>.Success(vehicles);
    }
}
