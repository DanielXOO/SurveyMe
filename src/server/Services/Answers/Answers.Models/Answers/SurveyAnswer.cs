using Answers.Models.Surveys;

namespace Answers.Models.Answers;

public sealed class SurveyAnswer
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public ICollection<BaseQuestionAnswer> QuestionsAnswers { get; set; }

    public Survey Survey { get; set; }

    public Guid SurveyId { get; set; }
}