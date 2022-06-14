using Answers.Models.Options;
using Answers.Models.Surveys;

namespace Answers.Models.Questions;

public sealed class Question
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public Survey Survey { get; set; }
    
    public Guid SurveyId { get; set; }

    public ICollection<Option> Options { get; set; }
}