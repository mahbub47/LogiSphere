namespace LogiSphere.Application.Features.Staff.Models;

public class StaffRegistrationResult
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string? Error { get; }

    private StaffRegistrationResult(bool isSuccess, string error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static StaffRegistrationResult Success() => new StaffRegistrationResult(true, string.Empty);
    public static StaffRegistrationResult Failed(string? error) => new StaffRegistrationResult(false, error ?? string.Empty);
}
