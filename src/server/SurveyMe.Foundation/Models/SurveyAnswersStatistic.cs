using System.Collections.Generic;

namespace SurveyMe.Foundation.Models;

public sealed class SurveyAnswersStatistic
{
    public string SurveyTitle { get; set; }

    public int AnswersCount { get; set; }

    public ICollection<QuestionAnswersStatistic> QuestionAnswersStatistic { get; set; }
}