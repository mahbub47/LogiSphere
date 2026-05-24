using LogiSphere.Application.Features.Tenants.Interfaces;
using LogiSphere.Domain.Entities;
using LogiSphere.Infrastructure.Data.DbContext;
using LogiSphere.Infrastructure.Data.Models;
using LogiSphere.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LogiSphere.Infrastructure.Data.Provisioners;

public class EnterpriceProvisioner(
    IOptions<IdentityOptions> optionsAccessor,
    IPasswordHasher<ApplicationUser> passwordHasher,
    IEnumerable<IUserValidator<ApplicationUser>> userValidators,
    IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
    ILookupNormalizer keyNormalizer,
    IdentityErrorDescriber errors,
    ILogger<UserManager<ApplicationUser>> logger) : IEnterpriceProvisioner
{
    public async Task<bool> ProvisionTenantDatabaseAsync(
        string dbConnectionString,
        Tenant tenant,
        string fullname,
        string email, 
        string phone, 
        string password)
    {
        try
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseNpgsql(dbConnectionString);

            using var tenantDbContext = new ApplicationDbContext(optionBuilder.Options)
                ?? throw new Exception("Database context creation failed");

            await tenantDbContext.Database.MigrateAsync();

            var repository = new Repository<Tenant>(tenantDbContext);
            var unitOfWork = new UnitOfWork(tenantDbContext);

            try
            {
                await unitOfWork.BeginTransactionAsync();

                await repository.AddAsync(tenant);
                await unitOfWork.SaveChangesAsync();

                var userStore = new UserStore<ApplicationUser, IdentityRole<Guid>, ApplicationDbContext, Guid>(tenantDbContext);

                var userManager = new UserManager<ApplicationUser>(
                    userStore,
                    optionsAccessor,
                    passwordHasher,
                    userValidators,
                    passwordValidators,
                    keyNormalizer,
                    errors,
                    null!,
                    logger);

                var user = new ApplicationUser
                {
                    FullName = fullname,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    TenantId = tenant.Id,
                };

                var userCreation = await userManager.CreateAsync(user, password);

                if (!userCreation.Succeeded)
                {
                    await unitOfWork.RollbackTransactionAsync();
                    throw new Exception("User Creation Failed");
                }

                var userRoleAssign = await userManager.AddToRoleAsync(user, "FleetManager");

                if (!userRoleAssign.Succeeded) 
                {
                    await unitOfWork.RollbackTransactionAsync();
                    throw new Exception("User role assign failed"); 
                }

                await unitOfWork.CommitTransactionAsync();
                return true;
            }
            catch
            {
                await unitOfWork.RollbackTransactionAsync();
                throw new Exception("User manager failed");
            }
        }
        catch
        {
            throw new Exception("Database opreration failed");
        }
    }
}
