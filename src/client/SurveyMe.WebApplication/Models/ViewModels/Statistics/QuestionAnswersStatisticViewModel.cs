using SurveyMe.DomainModels.Common;

namespace SurveyMe.WebApplication.Models.ViewModels.Statistics;

public sealed class QuestionAnswersStatisticViewModel
{
    public string QuestionTitle { get; set; }

    public QuestionType QuestionType { get; set; }
    
    public int AnswersCount { get; set; }
}