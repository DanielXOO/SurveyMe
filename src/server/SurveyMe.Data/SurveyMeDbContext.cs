using Microsoft.EntityFrameworkCore;
using SurveyMe.DomainModels.Answers;
using SurveyMe.DomainModels.Questions;
using SurveyMe.DomainModels.Roles;
using SurveyMe.DomainModels.Surveys;
using SurveyMe.DomainModels.Users;

namespace SurveyMe.Data;

public class SurveyMeDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    public SurveyMeDbContext(DbContextOptions<SurveyMeDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(b =>
        {
            b.HasMany(e => e.Roles)
                .WithMany(e => e.Users);

            b.HasMany(e => e.Surveys)
                .WithOne(e => e.Author)
                .IsRequired()
                .HasForeignKey(e => e.AuthorId);

            b.HasMany(e => e.Answers)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            ;

            b.Property(user => user.UserName).IsRequired();
            b.Property(user => user.DisplayName).IsRequired();
            b.Property(user => user.PasswordHash).IsRequired();
            b.Property(user => user.CreationTime).IsRequired();
        });

        modelBuilder.Entity<Role>(b => { b.Property(role => role.Name).IsRequired(); });

        modelBuilder.Entity<Survey>(b =>
        {
            b.HasMany(e => e.Questions)
                .WithOne(e => e.Survey)
                .IsRequired()
                .HasForeignKey(e => e.SurveyId);
            
            b.Property(survey => survey.LastChangeDate).IsRequired();
            b.Property(survey => survey.Name).IsRequired();
        });

        modelBuilder.Entity<Question>(b =>
        {
            b.HasMany(e => e.Options)
                .WithOne(e => e.Question)
                .HasForeignKey(e => e.QuestionId);
            
            b.HasMany(e => e.Answers)
                .WithOne(e => e.Question)
                .OnDelete(DeleteBehavior.NoAction);            
            
            b.Property(question => question.Title).IsRequired();
        });

        modelBuilder.Entity<SurveyAnswer>(b =>
        {
            b.HasMany(e => e.QuestionsAnswers)
                .WithOne(e => e.SurveyAnswer)
                .HasForeignKey(e => e.SurveyAnswerId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<BaseAnswer>(b =>
        {
            b.HasDiscriminator(e => e.QuestionType)
                .HasValue<TextAnswer>(QuestionType.Text)
                .HasValue<CheckboxAnswer>(QuestionType.Checkbox)
                .HasValue<RadioAnswer>(QuestionType.Radio)
                .HasValue<FileAnswer>(QuestionType.File)
                .HasValue<RateAnswer>(QuestionType.Rate)
                .HasValue<ScaleAnswer>(QuestionType.Scale);
        });

        modelBuilder.Entity<TextAnswer>();
        
        modelBuilder.Entity<CheckboxAnswer>(b =>
        {
            b.HasMany(e => e.Options)
                .WithOne(e => e.CheckboxAnswer)
                .HasForeignKey(e => e.CheckboxAnswerId);
        });
        
        modelBuilder.Entity<RadioAnswer>();
        
        modelBuilder.Entity<FileAnswer>();
        
        modelBuilder.Entity<RateAnswer>();
        
        modelBuilder.Entity<ScaleAnswer>();
        
        modelBuilder.Entity<OptionAnswer>();
    }
}