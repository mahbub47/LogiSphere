namespace LogiSphere.Application.Features.Tenants.Models;

public class TenantRegistrationResult
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Guid TenantId { get; }
    public string? Error { get; }

    private TenantRegistrationResult(bool  success, Guid tenantId, string error)
    {
        IsSuccess = success;
        TenantId = tenantId;
        Error = error;
    }

    public static TenantRegistrationResult Succeed(Guid tenantId) =>
        new(true, tenantId, string.Empty);

    public static TenantRegistrationResult Failed(string error) =>
        new(false, default, error);
}
