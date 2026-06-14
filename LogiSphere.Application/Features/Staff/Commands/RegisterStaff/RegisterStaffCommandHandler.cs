using LogiSphere.Application.Core.Abstraction;
using LogiSphere.Application.Core.Results;
using LogiSphere.Application.Interfaces;

namespace LogiSphere.Application.Features.Staff.Commands.RegisterStaff;

internal class RegisterStaffCommandHandler(IIdentityService identityService) : ICommandHandler<RegisterStaffCommand, Result<Guid>>
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
