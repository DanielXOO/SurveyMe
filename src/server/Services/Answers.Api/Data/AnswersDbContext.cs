using Answers.Api.Models.Answers;
using Answers.Api.Models.Questions;
using Microsoft.EntityFrameworkCore;

namespace Answers.Api.Data;

public class AnswersDbContext : DbContext
{
    public AnswersDbContext(DbContextOptions<AnswersDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SurveyAnswer>(b =>
        {
            b.HasMany(e => e.QuestionsAnswers)
                .WithOne(e => e.SurveyAnswer)
                .HasForeignKey(e => e.SurveyAnswerId);
        });

        modelBuilder.Entity<BaseQuestionAnswer>(b =>
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