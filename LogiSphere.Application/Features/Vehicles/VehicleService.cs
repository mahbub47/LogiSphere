using LogiSphere.Application.Contracts;
using LogiSphere.Application.Features.Vehicles.Models;
using LogiSphere.Application.Interfaces;
using LogiSphere.Domain.Entities;

namespace LogiSphere.Application.Features.Vehicles;

internal class VehicleService(
    IApplicationUnitOfWork unitOfWork
    ) : IVehicleService
{
    public async Task<IEnumerable<VehicleResponseDto>> GetAllVehicleAsync()
    {
        var vehicles = new List<VehicleResponseDto>();
        var vehicleEntities = await unitOfWork.Vehicles.GetAllAsync();
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
        return vehicles;
    }

    public async Task<VehicleRegistrationResult> RegisterVehicleAsync(VehicleRegistrationRequest request)
    {
        var vehicle = new Vehicle
        {
            PlateNumber = request.PlateNumber,
            Model = request.Model,
            MaxWeightCapacityKg = request.MaxWeightCapacityKg,
            IsActive = true,
        };
        try
        {
            await unitOfWork.Vehicles.AddAsync(vehicle);
            await unitOfWork.SaveChangesAsync();
        }
        catch
        {
            return VehicleRegistrationResult.Failure("Failed to register vehicle. Please try again.");
        }

        return VehicleRegistrationResult.Success(vehicle.Id);
    }
}
