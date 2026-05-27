using LogiSphere.Application.Features.Authentication.Interfaces;
using LogiSphere.Application.Features.Authentication.Models;
using LogiSphere.Application.Interfaces;

namespace LogiSphere.Application.Features.Authentication;

public class AuthenticationService(IIdentityService identityService) : IAuthenticationService
{
    public async Task<AuthenticationResult> AuthenticateUserAsync(AuthenticationRequest request)
    {
        (var isAuthenticated, var token) = await identityService.AuthenticateAsync(request.Email, request.Password);
        if (!isAuthenticated) return AuthenticationResult.Failed("User authentication failed");
        return AuthenticationResult.Succeed(token);
    }
}
