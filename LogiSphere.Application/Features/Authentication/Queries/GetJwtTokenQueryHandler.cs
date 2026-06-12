using LogiSphere.Application.Interfaces;
using LogiSphere.Application.Models;
using MediatR;

namespace LogiSphere.Application.Features.Authentication.Queries;

internal class GetJwtTokenQueryHandler(IIdentityService identityService) : IRequestHandler<GetJwtTokenQuery, Result<string>>
{
    public async Task<Result<string>> Handle(GetJwtTokenQuery request, CancellationToken cancellationToken)
    {
        (var isAuthenticated, var token) = await identityService.AuthenticateAsync(request.Email, request.Password);
        if (!isAuthenticated) return Result<string>.Failed(new Error("401", "Invalid credentials"));
        return Result<string>.Succeed(token);
    }
}
