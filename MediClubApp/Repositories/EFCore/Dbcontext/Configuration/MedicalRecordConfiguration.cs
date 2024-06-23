using MediClubApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediClubApp.Repositories.EFCore.Dbcontext.Configuration;

public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
{
    public void Configure(EntityTypeBuilder<MedicalRecord> builder)
    {
        builder.HasKey(mr => mr.Id);

        builder.Property(mr => mr.Date)
            .IsRequired();

        builder.Property(mr => mr.Diagnosis)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(mr => mr.Treatment)
            .HasMaxLength(500);

        builder.HasOne(mr => mr.Patient)
            .WithMany(patient => patient.MedicalRecords)
            .HasForeignKey(mr => mr.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(mr => mr.Doctor)
            .WithMany(doctor => doctor.MedicalRecords)
            .HasForeignKey(mr => mr.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}