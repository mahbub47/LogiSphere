using LogiSphere.Application.Contracts;
using LogiSphere.Application.Core.Result;
using LogiSphere.Application.DTOs;
using MediatR;

namespace LogiSphere.Application.Features.Vehicles.Queries.GetAllVehicle;

internal class GetAllVehicleQueryHandler(IApplicationUnitOfWork unitOfWork) : IRequestHandler<GetAllVehicleQuery, Result<IEnumerable<VehicleResponseDto>>>
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
