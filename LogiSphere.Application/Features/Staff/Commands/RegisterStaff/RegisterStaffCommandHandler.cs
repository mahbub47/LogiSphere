using LogiSphere.Application.Interfaces;
using LogiSphere.Application.Models;
using MediatR;

namespace LogiSphere.Application.Features.Staff.Commands.RegisterStaff;

internal class RegisterStaffCommandHandler(IIdentityService identityService) : IRequestHandler<RegisterStaffCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RegisterStaffCommand request, CancellationToken cancellationToken)
    {
        var result = await identityService.CreateStaffAsync(
            request.Role,
            request.FullName,
            request.Email,
            request.Phone,
            request.Password);

        if (result == Guid.Empty) return Result<Guid>.Failed(new Error("400","Failed to create staff account."));

        return Result<Guid>.Succeed(result);
    }
}
