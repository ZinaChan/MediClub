using FluentValidation;
using MediClubApp.Models;

namespace MediClubApp.Validators;

public class RoomValidator : AbstractValidator<Room>
{
    public RoomValidator()
    {
        RuleFor(r => r.DepartmentId)
              .NotEmpty().WithMessage("Department is required.");

        RuleFor(r => r.RoomNumber)
            .NotEmpty().WithMessage("Room Number is required.");
    }
}