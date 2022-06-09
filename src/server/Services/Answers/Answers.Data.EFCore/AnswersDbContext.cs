using Answers.Models.Answers;
using Answers.Models.Questions;
using Microsoft.EntityFrameworkCore;

namespace Answers.Data;

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
                .HasValue<TextQuestionAnswer>(QuestionType.Text)
                .HasValue<CheckboxQuestionAnswer>(QuestionType.Checkbox)
                .HasValue<RadioQuestionAnswer>(QuestionType.Radio)
                .HasValue<FileQuestionAnswer>(QuestionType.File)
                .HasValue<RateQuestionAnswer>(QuestionType.Rate)
                .HasValue<ScaleQuestionAnswer>(QuestionType.Scale);
        });

        modelBuilder.Entity<TextQuestionAnswer>();
        
        modelBuilder.Entity<CheckboxQuestionAnswer>(b =>
        {
            b.HasMany(e => e.Options)
                .WithOne(e => e.CheckboxQuestionAnswer)
                .HasForeignKey(e => e.CheckboxAnswerId);
        });
        
        modelBuilder.Entity<RadioQuestionAnswer>();
        
        modelBuilder.Entity<FileQuestionAnswer>();
        
        modelBuilder.Entity<RateQuestionAnswer>();
        
        modelBuilder.Entity<ScaleQuestionAnswer>();
        
        modelBuilder.Entity<OptionQuestionAnswer>();
    }
}