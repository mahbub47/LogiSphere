using LogiSphere.Domain.Entities;
using LogiSphere.Infrastructure.Data.Extensions;
using LogiSphere.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LogiSphere.Infrastructure.Data.Database.Context;

public class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : 
    IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Tenant> Tenants { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.HasOne(u => u.Tenant)
                .WithMany()
                .HasForeignKey(u => u.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.RegisteredAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        builder.SeedRoles();
    }
}
