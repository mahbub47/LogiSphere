using LogiSphere.Application.Features.Authentication;
using LogiSphere.Application.Features.Staff;
using LogiSphere.Application.Features.Tenants;
using Microsoft.Extensions.DependencyInjection;

namespace LogiSphere.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITenantRegistrationService, TenantRegistrationService>();

        services.AddScoped<IStaffRegistrationService, StaffRegistrationService>();

        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}
