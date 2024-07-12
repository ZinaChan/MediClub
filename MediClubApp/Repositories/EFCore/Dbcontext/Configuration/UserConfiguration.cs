using MediClubApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediClubApp.Repositories.EFCore.Dbcontext.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
                    .ValueGeneratedOnAdd()
                    .IsRequired()
                    .HasDefaultValueSql("NEWID()");

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(u => u.Email)
            .IsRequired() 
            .HasMaxLength(100);

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.LastName)
           .IsRequired()
           .HasMaxLength(50);

        builder.Property(d => d.DateOfBirth)
            .IsRequired();

        builder.Property(d => d.Gender)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(d => d.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);
            
        builder.Property(p => p.Address)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(u => u.AvatarUrl)
            .HasMaxLength(255);
    }
}