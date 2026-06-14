using LogiSphere.Application.Core.Result;
using MediatR;

namespace LogiSphere.Application.Features.Authentication.Queries;

public class GetJwtTokenQuery : IRequest<Result<string>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
