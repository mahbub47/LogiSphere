using LogiSphere.Application.Interfaces;
using LogiSphere.Domain.Entities;
using LogiSphere.Infrastructure.Data.Models;
using LogiSphere.Infrastructure.Interfaces;
using LogiSphere.Infrastructure.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

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

}
