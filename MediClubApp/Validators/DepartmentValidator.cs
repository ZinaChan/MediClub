using FluentValidation;
using MediClubApp.Models;

namespace MediClubApp.Validators;

public class DepartmentValidator : AbstractValidator<Department>
{
    public DepartmentValidator()
    {
        RuleFor(department => department.Name)
            .NotEmpty().WithMessage("Department Name is required.");
    }
}
