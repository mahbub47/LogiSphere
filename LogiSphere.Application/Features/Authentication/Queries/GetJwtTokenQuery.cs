using LogiSphere.Application.Core.Abstraction;
using LogiSphere.Application.Core.Results;

namespace LogiSphere.Application.Features.Authentication.Queries;

public class GetJwtTokenQuery : IQuery<Result<string>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
