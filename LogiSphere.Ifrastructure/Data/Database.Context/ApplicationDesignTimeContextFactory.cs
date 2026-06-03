using LogiSphere.Infrastructure.Interfaces;
using LogiSphere.Infrastructure.Tenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LogiSphere.Infrastructure.Data.Database.Context;

public class ApplicationDesignTimeContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../LogiSphere.Host"))
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            .Build();

        var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionBuilder.UseNpgsql(config.GetConnectionString("sharedDbConnectionString"));

        return new ApplicationDbContext(optionBuilder.Options, null!);
    }
}
