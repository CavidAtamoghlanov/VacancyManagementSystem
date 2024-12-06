using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using VacancyManagementSystem.Domain.Entities.Abstacts;
using VacancyManagementSystem.Domain.Entities.Concretes;

namespace VacancyManagementSystem.Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = 1, Name = "Admin", Role = "Administrator", IsDeleted = false, CreatedDate = DateTime.Now },
                new ApplicationRole { Id = 2, Name = "User", Role = "User", IsDeleted = false, CreatedDate = DateTime.Now }
            );

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser { Id = 1, UserName = "admin@domain.com", FirstName = "Admin", LastName = "User", IsDeleted = false, CreatedDate = DateTime.Now, PhoneNumber = "1234567890" },
                new ApplicationUser { Id = 2, UserName = "user@domain.com", FirstName = "John", LastName = "Doe", IsDeleted = false, CreatedDate = DateTime.Now, PhoneNumber = "0987654321" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Software", Description = "Software Development Jobs" },
                new Category { Id = 2, Name = "Marketing", Description = "Marketing Jobs" }
            );

            modelBuilder.Entity<Vacancy>().HasData(
                new Vacancy { Id = 1, Title = "Software Engineer", Description = "Developing software solutions", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1), IsActive = true, CategoryId = 1, QuestionCount = 5 },
                new Vacancy { Id = 2, Title = "Marketing Specialist", Description = "Creating marketing campaigns", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1), IsActive = true, CategoryId = 2, QuestionCount = 4 }
            );

            modelBuilder.Entity<QuestionBank>().HasData(
                new QuestionBank { Id = 1, QuestionText = "What is C#?", CategoryId = 1 },
                new QuestionBank { Id = 2, QuestionText = "What is Marketing?", CategoryId = 2 }
            );

            modelBuilder.Entity<AnswerOption>().HasData(
                new AnswerOption { Id = 1, OptionText = "Programming Language", IsCorrect = true, QuestionBankId = 1 },
                new AnswerOption { Id = 2, OptionText = "A Framework", IsCorrect = false, QuestionBankId = 1 },
                new AnswerOption { Id = 3, OptionText = "Field of Business", IsCorrect = true, QuestionBankId = 2 },
                new AnswerOption { Id = 4, OptionText = "A Programming Language", IsCorrect = false, QuestionBankId = 2 }
            );

            modelBuilder.Entity<VacancyQuestion>().HasData(
                new VacancyQuestion { Id = 1, VacancyId = 1, QuestionBankId = 1 },
                new VacancyQuestion { Id = 2, VacancyId = 2, QuestionBankId = 2 }
            );

            modelBuilder.Entity<Applicant>().HasData(
                new Applicant { Id = 1, FirstName = "Jane", LastName = "Doe", Email = "jane@domain.com", PhoneNumber = "1112223333", UserId = 1, VacancyId = 1, TestScore = 85, CVPath = "jane.pdf" },
                new Applicant { Id = 2, FirstName = "Mark", LastName = "Smith", Email = "mark@domain.com", PhoneNumber = "4445556666", UserId = 2, VacancyId = 2, TestScore = 90, CVPath = "mark.pdf" }
            );

            modelBuilder.Entity<TestAnswer>().HasData(
                new TestAnswer { Id = 1, ApplicantId = 1, VacancyQuestionId = 1, AnswerOptionId = 1, IsCorrect = true },
                new TestAnswer { Id = 2, ApplicantId = 2, VacancyQuestionId = 2, AnswerOptionId = 3, IsCorrect = true }
            );

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }

        private void UpdateAuditFields(EntityState state)
        {
            var entries = ChangeTracker.Entries()
                                       .Where(e => e.Entity is IBaseEntity<int> &&
                                                   (e.State == EntityState.Added || e.State == EntityState.Modified))
                                       .ToList();

            var currentUtcDate = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                var entity = (IBaseEntity<int>)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = currentUtcDate;
                }

                entity.ModifiedDate = currentUtcDate;

                if (entry.State == EntityState.Modified && entity.IsDeleted)
                {
                    entity.IsDeleted = true;
                }
            }
        }

        public override int SaveChanges()
        {
            UpdateAuditFields(EntityState.Modified);
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields(EntityState.Modified);
            return await base.SaveChangesAsync(cancellationToken);
        }

        // DbSet-lər
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<VacancyQuestion> VacancyQuestions { get; set; }
        public DbSet<QuestionBank> QuestionBanks { get; set; }
        public DbSet<TestAnswer> TestAnswers { get; set; }
    }
}
