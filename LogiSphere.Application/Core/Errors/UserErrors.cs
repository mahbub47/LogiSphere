
namespace LogiSphere.Application.Core.Errors;

public static class UserErrors
{
    public static readonly Error UserNotFound = new("User.UserNotFound", "User not found.");
    public static readonly Error UserNotAuthorized = new("User.UserNotAuthorized", "User is not authorized.");
    public static readonly Error UserCreationFailed = new("User.UserCreationFailed", "User creation failed.");
}
