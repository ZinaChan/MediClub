using System.Text.RegularExpressions;
using FluentValidation;
using MediClubApp.Models;

namespace MediClubApp.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
            .MaximumLength(20).WithMessage("Password cannot exceed 20 characters.");

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid Email format.");

        RuleFor(user => user.FirstName)
            .NotEmpty().WithMessage("FirstName is required.")
            .MinimumLength(2).WithMessage("FirstName must be at least 2 characters.")
            .MaximumLength(50).WithMessage("FirstName cannot exceed 50 characters.");

        RuleFor(user => user.LastName)
           .NotEmpty().WithMessage("LastName is required.")
           .MinimumLength(2).WithMessage("Username must be at least 2 characters.")
           .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");

        RuleFor(user => user.PhoneNumber)
            .NotEmpty().WithMessage("Phone Number is required.")
            .Matches(new Regex(@"(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]‌​)\s*)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)([2-9]1[02-9]‌​|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})\s*(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+)\s*)?$")).WithMessage("Invalid Phone Number format.");

        RuleFor(patient => patient.Address)
            .NotEmpty().WithMessage("Address is required.");

        RuleFor(patient => patient.Gender)
            .NotEmpty().WithMessage("Gender is required.")
            .Must(gender => Enum.TryParse(typeof(Gender), gender, true, out _))
            .WithMessage("Invalid Gender value.");
    }
}