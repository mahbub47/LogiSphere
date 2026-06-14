using LogiSphere.Application.Core.Abstraction;
using LogiSphere.Application.Core.Results;
using LogiSphere.Application.Interfaces;

namespace LogiSphere.Application.Features.Authentication.Queries;

internal class GetJwtTokenQueryHandler(IIdentityService identityService) : IQueryHandler<GetJwtTokenQuery, Result<string>>
{
    public async Task<Result<string>> Handle(GetJwtTokenQuery request, CancellationToken cancellationToken)
    {
        return await identityService.AuthenticateAsync(request.Email, request.Password);
    }
}
