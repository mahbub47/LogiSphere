
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LogiSphere.Infrastructure.Data;

public class DesignTimeContextFacctory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../LogiSphere.Host"))
            .AddJsonFile($"appsettings.json", optional: true)
            .AddJsonFile($"appsettings.Development.json")
            .Build();

        var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionBuilder.UseNpgsql(config.GetConnectionString("defaultconnection"));

        return new ApplicationDbContext(optionBuilder.Options);
    }
}
