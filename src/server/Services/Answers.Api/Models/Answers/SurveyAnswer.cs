namespace Answers.Api.Models.Answers;

public sealed class SurveyAnswer
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    
    public Guid SurveyId { get; set; }
    
    public ICollection<BaseQuestionAnswer> QuestionsAnswers { get; set; }
}