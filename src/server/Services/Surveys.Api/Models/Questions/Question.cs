using Surveys.Api.Models.Options;
using Surveys.Api.Models.Surveys;

namespace Surveys.Api.Models.Questions;

public class Question
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public QuestionType Type { get; set; }

    public Survey Survey { get; set; }

    public Guid SurveyId { get; set; }

    public ICollection<QuestionOption> Options { get; set; }
}