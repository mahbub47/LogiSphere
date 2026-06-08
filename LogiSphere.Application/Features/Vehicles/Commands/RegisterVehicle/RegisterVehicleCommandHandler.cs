using LogiSphere.Application.Contracts;
using LogiSphere.Application.Models;
using LogiSphere.Domain.Entities;
using MediatR;

namespace LogiSphere.Application.Features.Vehicles.Commands.RegisterVehicle;

internal class RegisterVehicleCommandHandler(IApplicationUnitOfWork unitOfWork) : IRequestHandler<RegisterVehicleCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RegisterVehicleCommand request, CancellationToken cancellationToken)
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
            return Result<Guid>.Failed(new Error("400", "Failed to register vehicle. Please try again."));
        }

        return Result<Guid>.Succeed(vehicle.Id);
    }
}
