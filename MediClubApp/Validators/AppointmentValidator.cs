using FluentValidation;
using MediClubApp.Models;

namespace MediClubApp.Validators;

public class AppointmentValidator : AbstractValidator<Appointment>
{
    public AppointmentValidator()
    {
        RuleFor(appointment => appointment.DoctorId)
            .NotEmpty().WithMessage("Doctor is required.");

        RuleFor(appointment => appointment.PatientId)
            .NotEmpty().WithMessage("Patient is required.");

        RuleFor(appointment => appointment.RoomId)
            .NotEmpty().WithMessage("Room is required.");

        RuleFor(appointment => appointment.Time)
            .NotEmpty().WithMessage("Time is required.")
            .Must(time => BeWithinWorkingHours(time)).WithMessage("Please select a time between 9 AM and 11 PM.");

        RuleFor(appointment => appointment.Reason)
            .NotEmpty().WithMessage("Reason is required.");

        RuleFor(appointment => appointment.Date)
            .NotEmpty().WithMessage("Date is required.")
            .Must(date => date.Date > DateTime.Today)
        .WithMessage("Date must be today or in the past.");
    }
     private bool BeWithinWorkingHours(TimeSpan time)
    {
        var startTime = new TimeSpan(9, 0, 0); // 9 AM
        var endTime = new TimeSpan(23, 0, 0); // 11 PM 

        return time >= startTime && time <= endTime;
    }
    
}
