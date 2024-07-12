using FluentValidation;
using MediClubApp.Models;

namespace MediClubApp.Validators;
public class SpecializationValidator : AbstractValidator<Specialization>
{
    public SpecializationValidator()
    {
        RuleFor(s => s.Name)
           .NotEmpty().WithMessage("Specialization Name is required.");

        RuleFor(s => s.DepartmentId)
              .NotEmpty().WithMessage("Department is required.");
    }
}