
using FluentValidation;

namespace LogiSphere.Application.Features.Staff.Commands.RegisterStaff;

public class RegisterStaffCommandValidator : AbstractValidator<RegisterStaffCommand>
{
    public RegisterStaffCommandValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name is required.");

        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$").WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.");

        RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone number is required.")
            .Matches((@"^([01]|\+88)?\d{11}")).WithMessage("Invalid phone number format.");

        RuleFor(x => x.Role).IsInEnum().WithMessage("Invalid user role.");
    }
}
