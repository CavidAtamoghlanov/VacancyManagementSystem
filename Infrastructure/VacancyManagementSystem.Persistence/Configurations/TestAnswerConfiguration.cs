using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VacancyManagementSystem.Domain.Entities.Concretes;

namespace VacancyManagementSystem.Persistence.Configurations;
public class TestAnswerConfiguration : IEntityTypeConfiguration<TestAnswer>
{
    public void Configure(EntityTypeBuilder<TestAnswer> builder)
    {
        builder.ToTable("TestAnswers");

        builder.HasKey(x => x.Id);

        builder.Property(ta => ta.IsCorrect)
            .IsRequired();

        builder.HasOne(ta => ta.Applicant)
            .WithMany(a => a.TestAnswers)
            .HasForeignKey(ta => ta.ApplicantId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}