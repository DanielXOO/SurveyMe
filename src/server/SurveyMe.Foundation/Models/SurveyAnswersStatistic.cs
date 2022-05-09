using System;

namespace SurveyMe.Foundation.Models;

public sealed class SurveyAnswersStatistic
{
    public Guid SurveyId { get; set; }

    public int AnswersCount { get; set; }

    public QuestionAnswersStatistic QuestionAnswersStatistic { get; set; }
}