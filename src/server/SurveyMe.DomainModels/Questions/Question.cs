using SurveyMe.DomainModels.Answers;
using SurveyMe.DomainModels.Surveys;

namespace SurveyMe.DomainModels.Questions;

public sealed class Question
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public QuestionType Type { get; set; }

    public Survey Survey { get; set; }

    public Guid SurveyId { get; set; }

    public ICollection<QuestionOption> Options { get; set; }
    
    public ICollection<QuestionAnswer> Answers { get; set; }
}