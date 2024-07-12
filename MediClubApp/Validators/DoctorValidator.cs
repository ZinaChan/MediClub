using System.Text.RegularExpressions;
using FluentValidation;
using MediClubApp.Models;

namespace MediClubApp.Validators;

public class DoctorValidator : AbstractValidator<Doctor>
{
    public DoctorValidator()
    {
        RuleFor(doctor => doctor.FirstName)
            .NotEmpty().WithMessage("First Name is required.");

        RuleFor(doctor => doctor.LastName)
            .NotEmpty().WithMessage("Last Name is required.");

        RuleFor(doctor => doctor.Gender)
            .NotEmpty().WithMessage("Gender is required.")
            .Must(gender => Enum.TryParse(typeof(Gender), gender, true, out _))
            .WithMessage("Invalid Gender value.");

        RuleFor(doctor => doctor.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid Email format.");

        RuleFor(doctor => doctor.PhoneNumber)
            .NotEmpty().WithMessage("Phone Number is required.")
            .Matches(new Regex(@"(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]‌​)\s*)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)([2-9]1[02-9]‌​|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})\s*(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+)\s*)?$")).WithMessage("Invalid Phone Number format.");

    }
}