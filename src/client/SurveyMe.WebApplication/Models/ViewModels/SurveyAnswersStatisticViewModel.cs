namespace SurveyMe.WebApplication.Models.ViewModels;

public sealed class SurveyAnswersStatisticViewModel
{
    public string SurveyTitle { get; set; }

    public int AnswersCount { get; set; }

    public ICollection<QuestionAnswersStatisticViewModel> QuestionAnswersStatistic { get; set; }
}