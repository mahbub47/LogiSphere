using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LogiSphere.Infrastructure.Data.Database.Context;

public class CatalogDesignTimeContextFacctory : IDesignTimeDbContextFactory<CatalogDbContext>
{
    public CatalogDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../LogiSphere.Host"))
            .AddJsonFile($"appsettings.json", optional: true)
            .AddJsonFile($"appsettings.Development.json")
            .Build();

        var optionBuilder = new DbContextOptionsBuilder<CatalogDbContext>();

        optionBuilder.UseNpgsql(config.GetConnectionString("catalogDbConnectionString"));

        return new CatalogDbContext(optionBuilder.Options);
    }
}
