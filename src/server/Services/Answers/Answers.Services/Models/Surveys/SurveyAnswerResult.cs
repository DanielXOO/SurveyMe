using Answers.Services.Models.Questions;

namespace Answers.Services.Models.Surveys;

public class SurveyAnswerResult
{
    public string Name { get; set; }

    public Guid UserId { get; set; }
    
    public ICollection<BaseAnswerResult> QuestionAnswers { get; set; }
}