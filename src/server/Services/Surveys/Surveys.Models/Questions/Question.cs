using Surveys.Models.Options;
using Surveys.Models.Surveys;

namespace Surveys.Models.Questions;

public class Question
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public QuestionType Type { get; set; }

    public Survey Survey { get; set; }

    public Guid SurveyId { get; set; }

    public ICollection<QuestionOption> Options { get; set; }
}