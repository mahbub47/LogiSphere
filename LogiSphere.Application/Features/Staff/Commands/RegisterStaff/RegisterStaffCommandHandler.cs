using LogiSphere.Application.Core.Result;
using LogiSphere.Application.Interfaces;
using MediatR;

namespace LogiSphere.Application.Features.Staff.Commands.RegisterStaff;

internal class RegisterStaffCommandHandler(IIdentityService identityService) : IRequestHandler<RegisterStaffCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RegisterStaffCommand request, CancellationToken cancellationToken)
    {
        return await identityService.CreateStaffAsync(
            request.Role,
            request.FullName,
            request.Email,  
            request.Phone,
            request.Password);
    }
}
