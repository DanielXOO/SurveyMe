namespace SurveyMe.DomainModels.Response;

public sealed class SurveyAnswersStatisticResponseModel
{
    public string SurveyTitle { get; set; }

    public int AnswersCount { get; set; }

    public ICollection<QuestionAnswersStatisticResponseModel> QuestionAnswersStatistic { get; set; }
}