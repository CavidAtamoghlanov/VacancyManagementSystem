using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VacancyManagementSystem.Domain.Entities.Concretes;

namespace VacancyManagementSystem.Persistence.Configurations;

public class VacancyQuestionConfiguration : IEntityTypeConfiguration<VacancyQuestion>
{
    public void Configure(EntityTypeBuilder<VacancyQuestion> builder)
    {
        builder.ToTable("VacancyQuestions");

        builder.HasKey(x => x.Id);

 

        builder.HasOne(vq => vq.Vacancy)
            .WithMany(v => v.VacancyQuestions)
            .HasForeignKey(vq => vq.VacancyId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}