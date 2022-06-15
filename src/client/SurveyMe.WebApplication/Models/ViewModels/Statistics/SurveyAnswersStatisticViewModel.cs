namespace SurveyMe.WebApplication.Models.ViewModels.Statistics;

public sealed class SurveyAnswersStatisticViewModel
{
    public string SurveyTitle { get; set; }

    public int AnswersCount { get; set; }

    public ICollection<QuestionAnswersStatisticViewModel> QuestionAnswersStatistic { get; set; }
}