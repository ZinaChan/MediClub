using FluentValidation;
using MediClubApp.Models;

namespace MediClubApp.Validators;

public class AppointmentValidator : AbstractValidator<Appointment>
{
    public AppointmentValidator()
    {
        RuleFor(appointment => appointment.DoctorId)
            .NotEmpty().WithMessage("Doctor is required.")
            .GreaterThan(0).WithMessage("Invalid Department Id.");

        RuleFor(appointment => appointment.PatientId)
            .NotEmpty().WithMessage("Patient is required.");

        RuleFor(appointment => appointment.RoomId)
            .NotEmpty().WithMessage("Room is required.");

        RuleFor(appointment => appointment.Time)
            .NotEmpty().WithMessage("Time is required.");

        RuleFor(appointment => appointment.Reason)
            .NotEmpty().WithMessage("Reason is required.");

        RuleFor(appointment => appointment.Date)
            .NotEmpty().WithMessage("Date is required.");
    }
}
