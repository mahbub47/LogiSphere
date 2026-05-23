
using LogiSphere.Application.Features.Tenants;
using LogiSphere.Application.Features.Tenants.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LogiSphere.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<ITenantRegistrationService, TenantRegistrationService>();

        return services;
    }
}
