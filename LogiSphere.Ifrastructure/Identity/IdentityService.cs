using LogiSphere.Application.Interfaces;
using LogiSphere.Domain.Enums;
using LogiSphere.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.ComponentModel;

namespace LogiSphere.Infrastructure.Identity;

public class IdentityService(
    IUserStore<ApplicationUser> store,
    IOptions<IdentityOptions> optionsAccessor,
    IPasswordHasher<ApplicationUser> passwordHasher,
    IEnumerable<IUserValidator<ApplicationUser>> userValidators,
    IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
    ILookupNormalizer keyNormalizer,
    IdentityErrorDescriber errors,
    IServiceProvider services,
    ILogger<UserManager<ApplicationUser>> logger,
    ICatalogUnitOfWork unitOfwork,
    IJwtTokenService tokenService) :
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
    public async Task<Guid> CreateTenantAdminAsync(Guid tenantId, string fullname, string email, string phone, string password)
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

        return user.Id;
    }

    public async Task<(bool, string)> AuthenticateAsync(string email, string password)
    {
        var user = await FindByEmailAsync(email);
        if (user == null) return (false, string.Empty);

        var isPasswordValid = await ValidatePasswordAsync(user, password);
        if(!isPasswordValid.Succeeded) return (false, string.Empty);

        var roles = await GetRolesAsync(user);
        if(roles.Count == 0) return (false, string.Empty);
        string userRole = roles.FirstOrDefault()!;

        var tenant = await unitOfwork.Tenants.GetByIdAsync(user.TenantId);
        if(tenant == null) return (false, string.Empty);

        string jwt = tokenService.GenerateToken(user, tenant, userRole);
        if(jwt  == null) return (false, string.Empty);

        return (true, jwt);
    }

    public async Task<Guid> CreateStaffAsync(UserRole role, string fullname, string email, string phone, string password)
    {
        var user = new ApplicationUser
        {
            FullName = fullname,
            Email = email,
            PhoneNumber = phone,
            UserName = email
        };

        var userCreation = await CreateAsync(user, password);

        if (!userCreation.Succeeded) throw new Exception("User Creation failed");

        var userRoleAssign = role switch
        {
            UserRole.FleetManager => await AddToRoleAsync(user, "FleetManager"),
            UserRole.Dispatcher => await AddToRoleAsync(user, "Dispatcher"),
            UserRole.Driver => await AddToRoleAsync(user, "Driver"),
            _ => throw new InvalidEnumArgumentException()
        };

        if (!userRoleAssign.Succeeded) throw new Exception("User role assign failed");

        return user.Id;
    }
}
