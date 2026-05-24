
using LogiSphere.Application.Features.Tenants.Interfaces;
using LogiSphere.Application.Interfaces;
using LogiSphere.Infrastructure.Data.DbContext;
using LogiSphere.Infrastructure.Data.Models;
using LogiSphere.Infrastructure.Data.Provisioners;
using LogiSphere.Infrastructure.Data.Repositories;
using LogiSphere.Infrastructure.Data.Services;
using LogiSphere.Infrastructure.Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LogiSphere.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("defaultconnection"));
        });

        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IIdentityService, IdentityService>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IEnterpriceProvisioner, EnterpriceProvisioner>();

        services.AddAuthentication();

        services.AddAuthorization();

        return services;
    }
}
