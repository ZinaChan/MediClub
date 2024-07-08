using MediClubApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediClubApp.Repositories.EFCore.Dbcontext.Configuration;
public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {

        builder.HasOne(d => d.Specialization)
            .WithMany(s => s.Doctors)
            .HasForeignKey(d => d.SpecializationId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(d => d.Department)
             .WithMany(s => s.Doctors)
             .HasForeignKey(d => d.DepartmentId)
             .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(d => d.Appointments)
              .WithOne(a => a.Doctor)
              .HasForeignKey(a => a.DoctorId);

        builder.HasMany(d => d.MedicalRecords)
              .WithOne(mr => mr.Doctor)
              .HasForeignKey(mr => mr.DoctorId);
    }
}
