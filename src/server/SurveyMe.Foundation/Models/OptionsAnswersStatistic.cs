using System;

namespace SurveyMe.Foundation.Models;

public sealed class OptionsAnswersStatistic
{
    public Guid OptionId { get; set; }
    
    public string Text { get; set; }

    public int AnswersCount { get; set; }
}