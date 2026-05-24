
using LogiSphere.Application.Interfaces;
using LogiSphere.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LogiSphere.Infrastructure.Data.Services;

public class IdentityService(
    IUserStore<ApplicationUser> store,
    IOptions<IdentityOptions> optionsAccessor,
    IPasswordHasher<ApplicationUser> passwordHasher,
    IEnumerable<IUserValidator<ApplicationUser>> userValidators,
    IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
    ILookupNormalizer keyNormalizer,
    IdentityErrorDescriber errors,
    IServiceProvider services,
    ILogger<UserManager<ApplicationUser>> logger) :
        UserManager<ApplicationUser>(
        store, 
        optionsAccessor,
        passwordHasher,
        userValidators,
        passwordValidators,
        keyNormalizer,
        errors,
        services,
        logger), IIdentityService
{
    public async Task<bool> CreateTenantAdminAsync(Guid tenantId, string fullname, string email, string phone, string password)
    {
        var user = new ApplicationUser
        {
            FullName = fullname,
            Email = email,
            PhoneNumber = phone,
            TenantId = tenantId,
            UserName = email
        };

        var userCreation = await CreateAsync(user, password);

        if (!userCreation.Succeeded) throw new Exception("User Creation failed");

        var userRoleAssign = await AddToRoleAsync(user, "FleetManager");

        if (!userRoleAssign.Succeeded) throw new Exception("User role assign failed");

        return userRoleAssign.Succeeded && userCreation.Succeeded;
    }
}
