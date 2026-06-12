using LogiSphere.Domain.Entities;
using LogiSphere.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogiSphere.Infrastructure.Data.Database.Context;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    ITenantResolver tenantResolver) : DbContext(options)
{
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<DeliveryStop> DeliveryStops { get; set; }
    public DbSet<DeliveryRoute> DeliveryRoutes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            var conectionString = tenantResolver.GetConnectionString();
            if (string.IsNullOrEmpty(conectionString))
            {
                throw new InvalidOperationException("Connection string is not configured for the current tenant.");
            }
            optionsBuilder.UseNpgsql(conectionString);
        }
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasQueryFilter(v => v.TenantId == tenantResolver.GetTenantId() && v.IsActive);

            entity.HasKey(v => v.Id);
            entity.Property(v => v.PlateNumber).IsRequired();
            entity.Property(v => v.Model).IsRequired();
            entity.Property(v => v.MaxWeightCapacityKg).IsRequired();
            entity.Property(v => v.IsActive).HasDefaultValue(true);
        });
        modelBuilder.Entity<DeliveryStop>(entity =>
        {
            entity.HasQueryFilter(ds => ds.TenantId == tenantResolver.GetTenantId());

            entity.HasKey(ds => ds.Id);
            entity.Property(ds => ds.CargoWeightKg).IsRequired();
            entity.Property(ds => ds.DestinationAddress).IsRequired();
            entity.Property(ds => ds.SequenceOrder).IsRequired();

            entity.HasOne(ds => ds.Route)
                .WithMany(r => r.DeliveryStops)
                .HasForeignKey(ds => ds.RouteId)
                .OnDelete(DeleteBehavior.SetNull);
        });
        modelBuilder.Entity<DeliveryRoute>(entity =>
        {
            entity.HasQueryFilter(dr => dr.TenantId == tenantResolver.GetTenantId());

            entity.HasKey(dr => dr.Id);
            
            entity.HasOne(dr => dr.Vehicle)
                .WithMany()
                .HasForeignKey(dr => dr.VehicleId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired();
        });
    }
}
