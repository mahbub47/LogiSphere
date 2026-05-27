namespace LogiSphere.Application.Features.Authentication.Models;

public class AuthenticationResult
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Token { get; }
    public string? Error { get; }

    private AuthenticationResult(bool isSuccess, string token, string error)
    {
        IsSuccess = isSuccess;
        Token = token;
        Error = error;
    }

    public static AuthenticationResult Succeed(string token) => 
        new AuthenticationResult(true, token, string.Empty);

    public static AuthenticationResult Failed(string error) =>
        new AuthenticationResult(false, string.Empty, error);
}
