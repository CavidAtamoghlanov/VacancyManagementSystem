using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VacancyManagementSystem.Domain.Entities.Concretes;

namespace VacancyManagementSystem.Persistence.Configurations;


public class AnswerOptionConfiguration : IEntityTypeConfiguration<AnswerOption>
{
    public void Configure(EntityTypeBuilder<AnswerOption> builder)
    {
        builder.ToTable("AnswerOptions");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.QuestionBank)
            .WithMany(q => q.AnswerOptions)
            .HasForeignKey(x => x.QuestionBankId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.OptionText)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.IsCorrect)
            .IsRequired();
    }
}