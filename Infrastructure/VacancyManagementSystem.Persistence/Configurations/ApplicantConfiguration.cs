using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VacancyManagementSystem.Domain.Entities.Concretes;

namespace VacancyManagementSystem.Persistence.Configurations;


public class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder.ToTable("Applicants");

        builder.HasKey(x => x.Id);

        builder.Property(a => a.TestScore).IsRequired();
        builder.Property(a => a.CVPath).HasMaxLength(500);

        builder.Property(a => a.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.PhoneNumber)
            .HasMaxLength(15);

        builder.Ignore(a => a.FullName);


        builder.HasOne(x => x.Vacancy)
            .WithMany(v => v.Applicants)
            .HasForeignKey(x => x.VacancyId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
