using Microsoft.EntityFrameworkCore;
using Surveys.Models.Questions;
using Surveys.Models.Surveys;

namespace Surveys.Data;

public class SurveysDbContext : DbContext
{
    public SurveysDbContext(DbContextOptions<SurveysDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
            
            b.Property(question => question.Title).IsRequired();
        });
    }
}