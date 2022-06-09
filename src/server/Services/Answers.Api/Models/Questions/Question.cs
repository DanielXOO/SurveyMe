using Answers.Api.Models.Answers;

namespace Answers.Api.Models.Questions;

public sealed class Question
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public QuestionType Type { get; set; }

    public Guid SurveyId { get; set; }
    
    public ICollection<BaseQuestionAnswer> Answers { get; set; }
}