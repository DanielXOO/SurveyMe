namespace SurveyMe.WebApplication.Models.Responses.Statistics;

public sealed class SurveyAnswersStatisticResponseModel
{
    public string SurveyTitle { get; set; }

    public int AnswersCount { get; set; }

    public ICollection<QuestionAnswersStatisticResponseModel> QuestionAnswersStatistic { get; set; }
}