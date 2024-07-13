using FluentValidation;
using MediClubApp.Models;

namespace MediClubApp.Validators;
public class MedicalRecordValidator : AbstractValidator<MedicalRecord>
{
    public MedicalRecordValidator()
    {
        RuleFor(medicalRecord => medicalRecord.PatientId)
            .NotEmpty().WithMessage("Patient is required.");

        RuleFor(medicalRecord => medicalRecord.DoctorId)
            .NotEmpty().WithMessage("Doctor is required.");

        RuleFor(medicalRecord => medicalRecord.Date)
            .NotEmpty().WithMessage("Date is required.")
            .Must(date => date.Date >= DateTime.Today)
        .WithMessage("Date must be today or in the future.");

        RuleFor(medicalRecord => medicalRecord.Diagnosis)
            .NotEmpty().WithMessage("Diagnosis is required.");

        RuleFor(medicalRecord => medicalRecord.Treatment)
            .NotEmpty().WithMessage("Treatment is required.");
    }
}