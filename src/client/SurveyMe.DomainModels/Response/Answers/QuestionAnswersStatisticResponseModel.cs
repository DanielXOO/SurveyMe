using SurveyMe.DomainModels.Common;

namespace SurveyMe.DomainModels.Response.Answers;

public sealed class QuestionAnswersStatisticResponseModel
{
    public string QuestionTitle { get; set; }

    public QuestionType QuestionType { get; set; }
    
    public int AnswersCount { get; set; }

    public double AverageRate { get; set; }

    public double AverageScale { get; set; }

    public ICollection<OptionAnswersStatisticResponseModel> OptionAnswersStatistic { get; set; }
}