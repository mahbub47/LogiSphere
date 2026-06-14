
using LogiSphere.Application.Core.Errors;

namespace LogiSphere.Application.Features.Authentication;

public static class AuthenticationnErrors
{
    public static readonly Error InvalidEmail = new("Authentication.InvalidEmail", "Invalid email provided.");

    public static readonly Error InvalidPassword = new("Authentication.InvalidPassword", "Invalid password provided.");

    public static readonly Error InvalidCredentials = new("Authentication.InvalidCredentials", "Invalid email or password.");

    public static readonly Error UserNotFound = new("Authentication.UserNotFound", "User not found.");
}
