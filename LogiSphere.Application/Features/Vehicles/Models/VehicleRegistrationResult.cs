namespace LogiSphere.Application.Features.Vehicles.Models;

public class VehicleRegistrationResult
{
    public bool IsSuccess { get; }
    public bool IsFailed => !IsSuccess;
    public Guid VehicleId { get; }
    public string? Error { get; }

    private VehicleRegistrationResult(bool isSuccess, Guid vehicleId, string? error)
    {
        IsSuccess = isSuccess;
        VehicleId = vehicleId;
        Error = error;
    }

    public static VehicleRegistrationResult Success(Guid vehicleId)
    {
        return new VehicleRegistrationResult(true, vehicleId, string.Empty);
    }

    public static VehicleRegistrationResult Failure(string error)
    {
        return new VehicleRegistrationResult(false, default, error);
    }
}
