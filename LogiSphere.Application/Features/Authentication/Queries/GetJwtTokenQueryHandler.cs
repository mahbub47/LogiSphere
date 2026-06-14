using LogiSphere.Application.Core.Result;
using LogiSphere.Application.Interfaces;
using MediatR;

namespace LogiSphere.Application.Features.Authentication.Queries;

internal class GetJwtTokenQueryHandler(IIdentityService identityService) : IRequestHandler<GetJwtTokenQuery, Result<string>>
{
    public async Task<Result<string>> Handle(GetJwtTokenQuery request, CancellationToken cancellationToken)
    {
        return await identityService.AuthenticateAsync(request.Email, request.Password);
    }
}
