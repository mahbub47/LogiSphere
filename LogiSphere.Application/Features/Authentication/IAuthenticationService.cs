using LogiSphere.Application.Features.Authentication.Models;

namespace LogiSphere.Application.Features.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticationResult> AuthenticateUserAsync(AuthenticationRequest request);
}
