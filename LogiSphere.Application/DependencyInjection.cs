using LogiSphere.Application.Features.Staff;
using LogiSphere.Application.Features.Tenants;
using LogiSphere.Application.Features.Vehicles;
using Microsoft.Extensions.DependencyInjection;

namespace LogiSphere.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITenantRegistrationService, TenantRegistrationService>();

        services.AddScoped<IVehicleService, VehicleService>();

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
        });

        return services;
    }
}
