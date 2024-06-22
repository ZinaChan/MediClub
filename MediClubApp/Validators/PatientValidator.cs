
using System.Text.RegularExpressions;
using FluentValidation;
using MediClubApp.Models;

namespace MediClubApp.Validators;

public class PatientValidator : AbstractValidator<Patient>
{
    public PatientValidator()
    {
        RuleFor(patient => patient.FirstName)
              .NotEmpty().WithMessage("First Name is required.");

        RuleFor(patient => patient.LastName)
            .NotEmpty().WithMessage("Last Name is required.");

        RuleFor(patient => patient.Gender)
            .NotEmpty().WithMessage("Gender is required.")
            .Must(gender => Enum.TryParse(typeof(Gender), gender, true, out _))
            .WithMessage("Invalid Gender value.");

        RuleFor(patient => patient.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid Email format.");

        RuleFor(patient => patient.PhoneNumber)
            .NotEmpty().WithMessage("Phone Number is required.")
            .Matches(new Regex(@"(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]‌​)\s*)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)([2-9]1[02-9]‌​|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})\s*(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+)\s*)?$")).WithMessage("Invalid Phone Number format.");

        RuleFor(patient => patient.Address)
            .NotEmpty().WithMessage("Address is required.");
    }
}
