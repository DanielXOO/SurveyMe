using System;
using SurveyMe.DomainModels;

namespace SurveyMe.Foundation.Models;

public sealed class QuestionAnswersStatistic
{
    public Guid QuestionId { get; set; }
    
    public string Title { get; set; }

    public int AnswersCount { get; set; }

    public OptionsAnswersStatistic OptionsAnswersStatistic { get; set; }
}