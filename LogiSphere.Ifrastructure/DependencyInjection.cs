using LogiSphere.Application.Contracts;
using LogiSphere.Application.Interfaces;
using LogiSphere.Infrastructure.Data.Database.Context;
using LogiSphere.Infrastructure.Data.Interceptors;
using LogiSphere.Infrastructure.Data.Repositories;
using LogiSphere.Infrastructure.Identity;
using LogiSphere.Infrastructure.Interfaces;
using LogiSphere.Infrastructure.Jwt;
using LogiSphere.Infrastructure.Tenancy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LogiSphere.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<SetTenantIdInterceptor>();

        services.AddScoped<ITenantResolver, TenantResolver>();

        services.AddDbContext<CatalogDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(config.GetConnectionString("catalogDbConnectionString"));

            var interceptor = serviceProvider.GetRequiredService<SetTenantIdInterceptor>();
            options.AddInterceptors(interceptor);
        });

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            var interceptor = serviceProvider.GetRequiredService<SetTenantIdInterceptor>();
            options.AddInterceptors(interceptor);
        });

        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<CatalogDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IJwtTokenService, JwtTokenService>();

        services.AddScoped<ICatalogUnitOfWork, CatalogUnitOfWork>();

        services.AddScoped<IApplicationUnitOfWork, ApplicationUnitOfWork>();

        services.AddScoped<IIdentityService, IdentityService>();

        services.AddScoped<IEnterpriseProvisioner, EnterpriseProvisioner>();

        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
    {
        var settings = new JwtSettings();
        config.GetSection("JwtSettings").Bind(settings);
        services.AddSingleton(settings);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret)),

                    ValidateIssuer = true,
                    ValidIssuer = settings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = settings.Audience,

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}
