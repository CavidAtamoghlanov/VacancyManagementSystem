using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VacancyManagementSystem.Domain.Entities.Concretes;

namespace VacancyManagementSystem.Persistence.Configurations;

public class QuestionBankConfiguration : IEntityTypeConfiguration<QuestionBank>
{
    public void Configure(EntityTypeBuilder<QuestionBank> builder)
    {
        builder.ToTable("QuestionBanks");

        builder.HasKey(x => x.Id);

        builder.Property(qb => qb.QuestionText)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasOne(qb => qb.Category)
            .WithMany(c => c.QuestionBanks)
            .HasForeignKey(qb => qb.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(qb => qb.AnswerOptions)
            .WithOne(a => a.QuestionBank)
            .HasForeignKey(a => a.QuestionBankId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}