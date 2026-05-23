using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace LogiSphere.Infrastructure.Data;

public static class ModelBuilderExtensions
{
    public static void SeedRoles(this ModelBuilder builder)
    {
        var roles = new List<IdentityRole<Guid>>() {
            new IdentityRole<Guid>
            {
                Id = Guid.Parse("67f2f82a-3a77-4aba-a16b-f37e1e65a5d3"),
                Name = "FleetManager",
                NormalizedName = "FLEETMANAGER",
                ConcurrencyStamp = "67f2f82a-3a77-4aba-a16b-f37e1e65a5d3",
            },
            new IdentityRole<Guid>
            {
                Id = Guid.Parse("4f8247fc-d879-4bfe-a630-d8b77844cfbf"),
                Name = "Dispatcher",
                NormalizedName = "DISPATCHER",
                ConcurrencyStamp = "4f8247fc-d879-4bfe-a630-d8b77844cfbf",
            },
            new IdentityRole<Guid>
            {
                Id = Guid.Parse("db4e84cd-75f8-4983-ace1-7fa3a6b1b885"),
                Name = "Driver",
                NormalizedName = "DRIVER",
                ConcurrencyStamp = "db4e84cd-75f8-4983-ace1-7fa3a6b1b885",
            },
        };

        builder.Entity<IdentityRole<Guid>>().HasData(roles);
    }
}
