using LogiSphere.Application.Features.Authentication;
using LogiSphere.Application.Features.Authentication.Interfaces;
using LogiSphere.Application.Features.Tenants;
using LogiSphere.Application.Features.Tenants.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LogiSphere.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITenantRegistrationService, TenantRegistrationService>();

        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}
