using SurveyMe.DomainModels.Questions;

namespace SurveyMe.Foundation.Models.Statistics;

public sealed class QuestionAnswersStatistic
{
    public string QuestionTitle { get; set; }

    public QuestionType QuestionType { get; set; }
    
    public int AnswersCount { get; set; }
}